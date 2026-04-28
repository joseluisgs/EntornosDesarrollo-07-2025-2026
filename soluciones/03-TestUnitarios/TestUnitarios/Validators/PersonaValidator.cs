using System.Text.RegularExpressions;
using TestUnitarios.Models;

namespace TestUnitarios.Validators;

public class PersonaValidator {
    public List<string> Validar(Persona persona) {
        var errores = new List<string>();

        errores.AddRange(ValidarNombre(persona.Nombre));
        errores.AddRange(ValidarEmail(persona.Email));
        errores.AddRange(ValidarPassword(persona.Password));
        errores.AddRange(ValidarDni(persona.Dni));
        errores.AddRange(ValidarEdad(persona.Edad));
        errores.AddRange(ValidarCalificacion(persona.Calificacion));

        return errores;
    }

    private List<string> ValidarNombre(string nombre) {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(nombre))
            errores.Add("El nombre no puede estar vacío");
        else if (nombre.Length < 2)
            errores.Add("El nombre debe tener al menos 2 caracteres");
        else if (nombre.Length > 50)
            errores.Add("El nombre no puede exceder 50 caracteres");

        return errores;
    }

    private List<string> ValidarEmail(string email) {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(email)) {
            errores.Add("El email no puede estar vacío");
        }
        else {
            if (!email.Contains("@"))
                errores.Add("El email debe contener '@'");
            if (!email.Contains("."))
                errores.Add("El email debe contener un dominio");
            if (email.StartsWith("@") || email.EndsWith("@"))
                errores.Add("El email no puede empezar o terminar con '@'");
            if (email.IndexOf("@", StringComparison.Ordinal) > email.LastIndexOf(".", StringComparison.Ordinal))
                errores.Add("El email debe tener el dominio después del '@'");
        }

        return errores;
    }

    private List<string> ValidarPassword(string password) {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(password)) {
            errores.Add("La contraseña no puede estar vacía");
        }
        else {
            if (password.Length < 8)
                errores.Add("La contraseña debe tener al menos 8 caracteres");
            if (password.Length > 20)
                errores.Add("La contraseña no puede exceder 20 caracteres");
            if (!password.Any(char.IsUpper))
                errores.Add("La contraseña debe tener al menos una mayúscula");
            if (!password.Any(char.IsLower))
                errores.Add("La contraseña debe tener al menos una minúscula");
            if (!password.Any(char.IsDigit))
                errores.Add("La contraseña debe tener al menos un número");
            if (password.All(char.IsLetterOrDigit))
                errores.Add("La contraseña debe tener al menos un carácter especial");
        }

        return errores;
    }

    private List<string> ValidarDni(string dni) {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(dni)) {
            errores.Add("El DNI no puede estar vacío");
            return errores;
        }

        if (dni.Length != 9) {
            errores.Add("El DNI debe tener 9 caracteres");
            return errores;
        }

        var numero = dni.Substring(0, 8);
        var letra = char.ToUpper(dni[8]);

        if (!Regex.IsMatch(dni, @"^\d{8}[A-Z]$")) {
            errores.Add("El DNI debe tener 8 números y una letra");
            return errores;
        }

        var letras = "TRWAGMYFPDXBNJZSQVHLCKE";
        if (int.TryParse(numero, out var numeroInt)) {
            var posicion = numeroInt % 23;
            var letraCalculada = letras[posicion];
            if (letra != letraCalculada)
                errores.Add("La letra del DNI no es correcta");
        }

        return errores;
    }

    private List<string> ValidarEdad(int edad) {
        var errores = new List<string>();

        if (edad <= 0)
            errores.Add("La edad debe ser mayor que 0");
        if (edad >= 150)
            errores.Add("La edad debe ser menor que 150");

        return errores;
    }

    private List<string> ValidarCalificacion(double calificacion) {
        var errores = new List<string>();

        if (calificacion < 0)
            errores.Add("La calificación no puede ser negativa");
        if (calificacion > 10)
            errores.Add("La calificación no puede ser mayor que 10");

        return errores;
    }
}