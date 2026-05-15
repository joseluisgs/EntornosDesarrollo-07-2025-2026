# Ejercicios Prácticos

**UD07 - Calidad de Software y Pruebas de Software**

---

## Parte 1: Ejercicios de Testing Unitario con NUnit

*Instrucciones:* Para los siguientes ejercicios, crea tests unitarios con NUnit y FluentAssertions.

### Ejercicio 1: Calculator con Tests

Dado el siguiente código de una calculadora, crea tests unitarios que cubran todos los casos (operaciones válidas e inválidas):

```csharp
public class Calculator
{
    public int Sum(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public decimal Divide(decimal a, decimal b)
    {
        if (b == 0) throw new DivideByZeroException("No se puede dividir por cero");
        return a / b;
    }
}
```

**Requisitos:**
- Tests para operaciones válidas (Sum, Subtract, Multiply, Divide con valores correctos)
- Test para Divide con divisor 0 (debe lanzar excepción)
- Usa FluentAssertions para las aserciones
- Aplica el patrón AAA

---

### Ejercicio 2: StringHelper con Tests

Crea tests para la siguiente clase:

```csharp
public class StringHelper
{
    public bool IsPalindrome(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        var reversed = new string(text.Reverse().ToArray());
        return text.Equals(reversed, StringComparison.OrdinalIgnoreCase);
    }
    
    public string Reverse(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return new string(text.Reverse().ToArray());
    }
    
    public int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
```

**Requisitos:**
- Tests para IsPalindrome: casos válidos ("radar", "hello"), palíndromos con mayúsculas/minúsculas
- Tests para Reverse: cadena normal, cadena vacía, cadena null
- Tests para CountWords: texto normal, texto con múltiples espacios, texto vacío

---

### Ejercicio 3: Validator con Tests

```csharp
public class Validator
{
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return email.Contains("@") && email.Contains(".");
    }
    
    public bool IsValidAge(int age)
    {
        return age >= 0 && age <= 150;
    }
    
    public (bool IsValid, List<string> Errors) ValidateUser(string name, string email, int age)
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(name))
            errors.Add("El nombre es obligatorio");
            
        if (!IsValidEmail(email))
            errors.Add("El email no es válido");
            
        if (!IsValidAge(age))
            errors.Add("La edad debe estar entre 0 y 150");
            
        return (errors.Count == 0, errors);
    }
}
```

**Requisitos:**
- Tests usando `[TestCase]` para múltiples valores de email
- Tests para el método `ValidateUser` que verifica tanto el boolean como la lista de errores
- Usa FluentAssertions para verificar el resultado

---

## Parte 2: Ejercicios de Moq y Test Doubles

### Ejercicio 4: Servicio con Dependencias

Dado el siguiente código, crea tests con Moq:

```csharp
public interface IUserRepository
{
    User? GetById(int id);
    IEnumerable<User> GetAll();
    void Save(User user);
    void Delete(int id);
}

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IEmailService _emailService;
    
    public UserService(IUserRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public User? GetUser(int id)
    {
        return _repository.GetById(id);
    }
    
    public void CreateUser(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        _repository.Save(user);
        _emailService.SendEmail(email, "Bienvenido", $"Hola {name}, bienvenido!");
    }
}
```

**Requisitos:**
- Test que verifique que `GetUser` retorna el usuario correcto
- Test que verifique que `GetUser` retorna null cuando no existe
- Test que verifique que `CreateUser` guarda el usuario Y envía un email
- Usa `Verify` para verificar las interacciones con el mock

---

### Ejercicio 5: Configuración de Mocks

Usando la clase `UserService` del ejercicio anterior:

**Requisitos:**
- Configura el mock del repositorio para retornar un usuario específico
- Configura el mock del servicio de email
- Verifica con `Times.Once`, `Times.Never`, `Times.Exactly(n)`
- Usa `It.IsAny<string>()` y `It.Is<T>(condición)` en los setups

---

## Parte 3: Ejercicios de Análisis de Caja Negra

### Ejercicio 6: Diseñar Casos de Prueba

Diseña casos de prueba usando Particionamiento de Equivalencia (EP) y Análisis de Valores Límite (BVA) para:

**Sistema de descuentos:**
- 0-50€: 0% descuento
- 50.01-100€: 5% descuento
- 100.01-200€: 10% descuento
- > 200€: 15% descuento

**Requisitos:**
- Identifica las particiones válidas e inválidas
- Selecciona valores representativos para cada partición
- Aplica BVA para los valores límite
- Crea una tabla con los casos de prueba

---

### Ejercicio 7: Calcular Complejidad Ciclomática

Calcula la complejidad ciclomática del siguiente código:

```csharp
public string ClassifyGrade(int score)
{
    if (score < 0 || score > 100)
        return "Inválido";
    
    if (score < 5)
        return "Suspenso";
    
    if (score < 7)
        return "Aprobado";
    
    if (score < 9)
        return "Notable";
    
    return "Sobresaliente";
}
```

**Requisitos:**
- Identifica los nodos y aristas
- Calcula V(G) = E - N + 2
- Indica cuántos "bases paths" independientes hay
- Diseña tests que cubran todos los paths

---

## Parte 4: Ejercicios de Serilog

### Ejercicio 8: Configurar Logger

Crea una clase de utilidad que configure Serilog con:

```csharp
public static class LoggerConfig
{
    public static void Configure(string logPath)
    {
        // Configurar Serilog con:
        // - Consola con nivel Debug
        // - Archivo con nivel Error mínimo
        // - Formato: {Timestamp:HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message}
        // - RollingInterval.Day
        // - RetainedFileCountLimit = 7
    }
}
```

**Requisitos:**
- Implementa la configuración hardcodeada
- Implementa la configuración desde `appsettings.json`
- Usa `Log.ForContext<T>()` en una clase de servicio
- Verifica que los logs se escriben correctamente

---

## Parte 5: Ejercicios de Cobertura

### Ejercicio 9: Mejorar Cobertura

Dado el siguiente código con cobertura parcial:

```csharp
public class OrderService
{
    public decimal CalculateDiscount(decimal amount, string customerType)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
            
        decimal discount = 0;
        
        if (customerType == "VIP")
            discount = 0.20m;
        else if (customerType == "Regular")
            discount = 0.10m;
        else if (customerType == "New")
            discount = 0.05m;
            
        if (amount > 1000)
            discount += 0.05m;
            
        return amount * (1 - discount);
    }
}
```

**Requisitos:**
- Escribe tests que alcancen el 100% de cobertura
- Identifica qué branches no están cubiertos
- Ejecuta los tests con `--collect:"XPlat Code Coverage"`
- Genera un informe HTML con ReportGenerator

---

## Parte 6: Ejercicios Integrados

### Ejercicio 10: Proyecto Completo

Implementa un sistema de gestión de tareas (`TaskManager`) con:

```csharp
public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Save(TaskItem task);
    void Delete(int id);
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
}

public enum TaskPriority { Low, Medium, High }

public class TaskService
{
    private readonly ITaskRepository _repository;
    private readonly ILogger<TaskService> _logger;
    
    public TaskService(ITaskRepository repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public IEnumerable<TaskItem> GetOverdueTasks()
    {
        var tasks = _repository.GetAll();
        return tasks.Where(t => !t.IsCompleted && t.DueDate < DateTime.Now);
    }
    
    public void CompleteTask(int id)
    {
        var task = _repository.GetById(id);
        if (task == null)
            throw new KeyNotFoundException($"Task with id {id} not found");
            
        task.IsCompleted = true;
        _repository.Save(task);
        _logger.LogInformation("Task {TaskId} completed", id);
    }
}
```

**Requisitos:**
1. Crea tests unitarios con NUnit y Moq
2. Usa FluentAssertions para las aserciones
3. Aplica el patrón AAA
4. Usa `Verify` para verificar interacciones
5. Configura Serilog en la clase de tests
6. Genera informe de cobertura

---

> **Nota del Profesor:** Estos ejercicios practican los conceptos vistos en la teoría. Recordad que la práctica hace al maestro. Intentad hacer los ejercicios primero sin mirar las soluciones y luego comparad.
