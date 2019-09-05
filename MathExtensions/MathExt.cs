using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MathExtensions
{
    public static class MathExt
    {
        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeSimple(long number)
        {
            double max = Math.Sqrt(number);
            for (long i = 2; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static readonly object _lock = new object();
        private static readonly ReaderWriterLockSlim _primeLock = new ReaderWriterLockSlim();
        private static HashSet<long> _primes = new HashSet<long>() { 2 };

        public static bool IsPrime(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0)
                return false;

            // The value up to which we to check to determine if the number is prime
            double max = Math.Sqrt(number);

            long tempPrime = 2;

            _primeLock.EnterReadLock();
            try
            {
                // First, iterate over existing primes
                foreach (var prime in _primes)
                {
                    if (prime == number) 
                    {
                        return true; // The number is a prime and is already included
                    }

                    if (number % prime == 0)
                    {
                        return false; // The number is divisible, by another number except 1. Hence it is not a prime
                    }

                    if (prime > max)
                    {
                        break; // The current prime is larger than max, we can stop checking
                    }

                    tempPrime = prime; // Set tempPrime to hold the value of the last iteration
                }
            }
            finally
            {
                _primeLock.ExitReadLock();
            }

            for (long i = tempPrime; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            _primeLock.EnterWriteLock();
            try
            {
                if (!_primes.Contains(number))
                {
                    _primes.Add(number);
                }
            }
            finally
            {
                _primeLock.ExitWriteLock();
            }
            return true;
        }
    }
}



