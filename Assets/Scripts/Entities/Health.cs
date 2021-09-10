﻿using System;
using UnityEngine;
using Utilities;

namespace Entities
{
    public abstract class Health : MonoCache
    {
        [field: Range(0,9999)][field: SerializeField] public int MaxHealth { get; private set; }
        [SerializeField] private int startingHealth = 100;

        private int _currentHealth;
        
        protected override void Awake()
        {
            base.Awake();
            _currentHealth = Mathf.Clamp(startingHealth, 0, MaxHealth);
            OnHealthChanged();
        }

        public void RecoverHealth(int healthAddition)
        {
            _currentHealth += healthAddition;
            if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
            OnHealthChanged();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }

        protected abstract void Die();

        private int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    OnHealthChanged();
                    Die();
                }
                OnHealthChanged();
            } 

        }

        protected virtual void OnHealthChanged(HealthChangedEventArgs e = null)
        {
            if (e == null)
            {
                HealthChangedEventArgs args = new HealthChangedEventArgs
                {
                    Health = _currentHealth, MaxHealth = MaxHealth
                };
                HealthChanged?.Invoke(this, args);
            }
            else
            {
                HealthChanged?.Invoke(this, e);
            }

        }
        
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        public class HealthChangedEventArgs : EventArgs
        {
            public int Health {get; set;}
            public int MaxHealth { get; set; }
        }

    }
}