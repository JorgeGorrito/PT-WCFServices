using PruebaTecnica.BDO.Exceptions;
using System;
using System.Collections.Generic;

namespace PruebaTecnica.BDO.Entities
{
    public class User
    {
        private static readonly List<char> allowedGenders = new List<char>(){'M', 'F'};

        private char gender = 'F';
        private DateTime birthDate = DateTime.MinValue;
        private string name = string.Empty;

        public int ID { get; set; }
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationErrorException("El nombre del usuario no debe estar vacio.");

                name = value;
            }
        }

        public DateTime BirthDate {
            get => birthDate;
            set {
                DateTime today = DateTime.Today;

                if (value.Date > today)
                    throw new ValidationErrorException("Fecha de nacimiento no puede ser futura.");

                birthDate = value.Date;
            } 
        }

        public char Gender { 
            get => gender;
            set {
                char valueFormatted = char.ToUpper(value);
                if (!allowedGenders.Contains(valueFormatted))
                    throw new ValidationErrorException("Género inválido. Use 'M' o 'F'.");

                gender = valueFormatted;
            }
        }
    }
}