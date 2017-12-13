using System;

namespace Herotale.Models
{
    public class Combat
    {
        public Character Chr { get; set; }
        public Enemy Enem { get; set; }
<<<<<<< HEAD
=======

        public int Heal(Character Chaa)
        {
            int currenthp = Chaa.Health;
            int maxhp = Chaa.MaxHealth;

            if (currenthp == maxhp)
            {
                //message that healing isnt necessary.
                return currenthp;
            }
            currenthp += (maxhp / 4);

            if (currenthp > maxhp)
            {
                currenthp = maxhp;
            }

            return currenthp;
        }

        public bool Fight(Character Char, Enemy Enm) //return true = Player won, return false = Enemy won.
        {
            Chr = Char;
            Enem = Enm;

            if (StartBattle())
            {
                //show message that you got first attack
                PlayerAttack();
            }
            //show message enemy attacked first
            EnemyAttack();


            return true;
        }

        public bool StartBattle()
        {
            Random ran = new Random();

            if (ran.Next(0, 2) != 0)
            {
                return true;
            }
            return false;
        }

        public void PlayerAttack()
        {
            if (Hit())
            {
                Enem.Health -= CalculatePlayerDamage();

                if (Enem.Health <= 0)
                {
                    EnemyDeath();
                }
            }
        }

        public void EnemyAttack()
        {
            if (Hit())
            {
                Chr.Health -= CalculateEnemyDamage();

                if (Enem.Health <= 0)
                {
                    PlayerDeath();
                }
            }
        }

        public bool Hit()
        {
            Random ran = new Random();

            if (ran.Next(0, 2) != 0)
            {
                return true;
            }
            return false;
        }

        public int CalculatePlayerDamage()
        {
            int focus = Chr.Cl.Focus;
            int damage = 0;

            switch (focus)
            {
                case 1:
                {
                    damage = 10 + Chr.AttackPower;
                    break;
                }
                case 2:
                {
                    damage = 10 + Chr.Defense;
                    break;
                }
                case 3:
                {
                    damage = 10 + Chr.Speed;
                    break;
                }
            }

            return damage;
        }

        public int CalculateEnemyDamage()
        {
            int damage = 10 + Enem.AttackPower;
            return damage;
        }

        public void EnemyDeath()
        {
            //rip
        }

        public void PlayerDeath()
        {
            //rip
        }
    }
>>>>>>> master
}