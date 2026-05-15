# TestDobles - Proyecto de Test Doubles y Mocks

## 📚 Objetivo del Proyecto

Este proyecto demuestra los conceptos de **Test Doubles** (dobles de prueba) en C# utilizando:
- **Moq** para crear mocks
- **NUnit** como framework de testing
- **FluentAssertions** para aserciones fluidas
- **CSharpFunctionalExtensions** para el patrón **Result**
- **Patrón AAA** con sección **Verify** para verificar llamadas a mocks

## 🏗️ Estructura del Proyecto

```
TestDobles/
├── TestDobles/                          # Proyecto principal
│   ├── Models/
│   │   └── Persona.cs                   # Entidad con CalificaciónTexto
│   ├── Repositories/
│   │   ├── ICrudRepository.cs           # Interfaz genérica CRUD
│   │   ├── IPersonaRepository.cs        # Interfaz específica Persona
│   │   └── PersonaRepository.cs         # Implementación en memoria
│   ├── Services/
│   │   ├── IPersonaService.cs           # Interfaz del servicio (Result)
│   │   └── PersonaService.cs            # Implementación con validaciones
│   ├── Validators/
│   │   ├── IValidator.cs                # Interfaz genérica validador
│   │   ├── IPersonaValidator.cs         # Interfaz específica Persona
│   │   └── PersonaValidator.cs          # Implementación de validación
│   └── Errors/
│       └── PersonaError.cs              # Errores de dominio
│
└── TestDobles.Test/                      # Proyecto de tests
    ├── Services/
    │   └── PersonaServiceTests.cs       # Tests con Mocks y Result
    ├── Repositories/
    │   └── PersonaRepositoryTests.cs    # Tests con implementación real
    ├── Models/
    │   └── PersonaTests.cs              # Tests del modelo
    └── Validators/
        └── PersonaValidatorTests.cs     # Tests del validador
```

## 📖 Conceptos Aprendidos

### 1. Errores de Dominio con Result<T, TError>

El patrón **Result** es una alternativa a las excepciones que maneja errores de forma funcional:

```csharp
// Definición de errores
public abstract record PersonaError(string Message)
{
    public sealed record NotFound(int Id) 
        : PersonaError($"No se ha encontrado ninguna persona con el identificador: {Id}");
    
    public sealed record NotFoundEmail(string Email) 
        : PersonaError($"No se ha encontrado ninguna persona con el email: {Email}");
    
    public sealed record Validation(IEnumerable<string> Errors) 
        : PersonaError("Se han detectado errores de validación en la entidad.");
}

// Interfaz que retorna Result
public interface IPersonaService
{
    Result<Persona, PersonaError> GetById(int id);
    Result<Persona, PersonaError> Create(Persona persona);
}
```

### 2. Implementación con Result

```csharp
public Result<Persona, PersonaError> GetById(int id)
{
    var persona = repository.GetById(id);
    if (persona == null)
        return Result.Failure<Persona, PersonaError>(PersonaError.NotFound(id));
    
    return Result.Success<Persona, PersonaError>(persona);
}
```

### 3. Tests con Result

```csharp
// ✅ Caso éxito
[Test]
public void GetById_Existe_RetornaPersona()
{
    var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
    _mockRepository.Setup(r => r.GetById(1)).Returns(persona);

    var resultado = _service.GetById(1);

    resultado.IsSuccess.Should().BeTrue();
    resultado.Value.Nombre.Should().Be("Juan");
}

// ❌ Caso error
[Test]
public void GetById_NoExiste_RetornaError()
{
    _mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

    var resultado = _service.GetById(999);

    resultado.IsFailure.Should().BeTrue();
    resultado.Error.Should().BeOfType<PersonaError.NotFound>();
}
```

### 4. ¿Qué son los Test Doubles?

Los **Test Doubles** son objetos que reemplazan a las dependencias reales durante los tests. Tipos:

| Tipo | Descripción | Uso |
|------|-------------|-----|
| **Dummy** | Objetos pasivos que no se usan realmente | Completar firmas de métodos |
| **Fake** | Implementación funcional pero simplificada | PersonaRepository en memoria |
| **Stub** | Responde con datos predefinidos | `Setup()` que retorna valores fijos |
| **Mock** | Verifica interacciones y llamadas | `Verify()` para confirmar llamadas |
| **Spy** | Registra información para verificar | Similar al mock, pero menos común |

### 5. Diferencia entre Stub y Mock

```csharp
// STUB - Proporciona datos predefinidos
[Test]
public void GetAll_HayPersonas_RetornaTodas()
{
    var personas = new List<Persona> { ... };
    _mockRepository.Setup(r => r.GetAll()).Returns(personas); // STUB

    var resultado = _service.GetAll();

    resultado.Should().HaveCount(2);
}
```

```csharp
// MOCK - Verifica que se llamaron los métodos correctos
[Test]
public void Create_Valida_RetornaCreada()
{
    _mockValidator.Setup(v => v.Validar(persona)).Returns(new List<string>()); // STUB
    _mockRepository.Setup(r => r.Create(persona)).Returns(personaCreada); // STUB

    var resultado = _service.Create(persona);

    resultado.IsSuccess.Should().BeTrue();

    // Verify - Verifica las interacciones (MOCK)
    _mockValidator.Verify(v => v.Validar(persona), Times.Once);
    _mockRepository.Verify(r => r.Create(persona), Times.Once);
}
```

### 6. Usando Moq

#### Crear un Mock
```csharp
var mockRepository = new Mock<IPersonaRepository>();
```

#### Configurar el comportamiento (Stub)
```csharp
// Retornar un valor
mockRepository.Setup(r => r.GetById(1)).Returns(persona);

// Retornar null
mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

// Retornar una lista
mockRepository.Setup(r => r.GetAll()).Returns(personas);
```

#### Verificar interacciones (Mock)
```csharp
// Verificar que se llamó una vez
_mockRepository.Verify(r => r.GetById(1), Times.Once);

// Verificar que NO se llamó
_mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);

// Verificar número exacto de llamadas
_mockRepository.Verify(r => r.GetAll(), Times.Exactly(2));
```

#### Usar It para matchers
```csharp
// Any
It.IsAny<Persona>()

// Matching con condición
It.Is<Persona>(p => p.Nombre == "Juan")

// Matching con rango
It.IsInRange(1, 100, Range.Inclusive)
```

### 7. Escenarios de Testing Prácticos

#### Escenario 1: Test de caso éxito (Happy Path)
```csharp
[Test]
public void GetById_Existe_RetornaPersona()
{
    // Arrange - Preparar datos y stubs
    var persona = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
    _mockRepository.Setup(r => r.GetById(1)).Returns(persona);

    // Act - Ejecutar la acción
    var resultado = _service.GetById(1);

    // Assert - Verificar resultado
    resultado.IsSuccess.Should().BeTrue();
    resultado.Value.Nombre.Should().Be("Juan");

    // Verify - Verificar interacciones
    _mockRepository.Verify(r => r.GetById(1), Times.Once);
}
```

#### Escenario 2: Test de caso error (Not Found)
```csharp
[Test]
public void GetById_NoExiste_RetornaError()
{
    // Arrange
    _mockRepository.Setup(r => r.GetById(999)).Returns((Persona?)null);

    // Act
    var resultado = _service.GetById(999);

    // Assert
    resultado.IsFailure.Should().BeTrue();
    resultado.Error.Should().BeOfType<PersonaError.NotFound>();
    
    // Verify
    _mockRepository.Verify(r => r.GetById(999), Times.Once);
}
```

#### Escenario 3: Test de validación (Validation Error)
```csharp
[Test]
public void Create_ConErroresValidacion_RetornaError()
{
    // Arrange
    var persona = new Persona(0, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
    var errores = new List<string> { "El nombre no puede estar vacío" };
    
    _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

    // Act
    var resultado = _service.Create(persona);

    // Assert
    resultado.IsFailure.Should().BeTrue();
    resultado.Error.Should().BeOfType<PersonaError.Validation>();
    
    // Verify - Verificar que NO se chamou al repositorio
    _mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);
}
```

#### Escenario 4: Test con matcher avanzado
```csharp
[Test]
public void Update_ConPersonaValida_RetornaActualizada()
{
    // Arrange
    var personaOriginal = new Persona(1, "Juan", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
    var personaActualizada = new Persona(1, "Juan Actualizado", "juan@email.com", "Password1!", "00000000T", 26, 8.0);
    
    _mockValidator.Setup(v => v.Validar(It.Is<Persona>(p => p.Nombre == "Juan"))).Returns(new List<string>());
    _mockRepository.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Persona>())).Returns(personaActualizada);

    // Act
    var resultado = _service.Update(1, personaOriginal);

    // Assert
    resultado.IsSuccess.Should().BeTrue();
    resultado.Value.Nombre.Should().Be("Juan Actualizado");
}
```

#### Escenario 5: Verificar que NO se llamó a un método
```csharp
[Test]
public void Create_ConErroresValidacion_NoGuardaEnRepositorio()
{
    // Arrange
    var persona = new Persona(0, "", "juan@email.com", "Password1!", "00000000T", 25, 7.5);
    var errores = new List<string> { "El nombre no puede estar vacío" };
    
    _mockValidator.Setup(v => v.Validar(persona)).Returns(errores);

    // Act
    var resultado = _service.Create(persona);

    // Assert
    resultado.IsFailure.Should().BeTrue();
    
    // Verify - IMPORTANTE: Verificar que NO se llamó al repositorio
    _mockRepository.Verify(r => r.Create(It.IsAny<Persona>()), Times.Never);
}
```

### 8. Primary Constructors (C# 14)

```csharp
public class PersonaService(IPersonaRepository repository, IPersonaValidator validator) : IPersonaService
{
    public Result<Persona, PersonaError> GetById(int id)
    {
        var persona = repository.GetById(id);
        if (persona == null)
            return Result.Failure<Persona, PersonaError>(PersonaError.NotFound(id));
        return Result.Success<Persona, PersonaError>(persona);
    }
}
```

## 🧪 Cómo Ejecutar los Tests

### Ejecutar todos los tests:
```bash
dotnet test
```

### Ejecutar con cobertura:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Ver informe HTML de cobertura:
```bash
reportgenerator -reports:"TestDobles.Test/TestResults/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
start coveragereport/index.html
```

## 📊 Cobertura de Código

### Resultados Actuales:
- **Tests superados:** 80

### ¿Por qué es útil el patrón Result?

El patrón **Result** ofrece ventajas sobre las excepciones:
- ✅ **Explicito**: El retorno de error es parte de la firma del método
- ✅ **Funcional**: Permite encadenar operaciones sin excepciones
- ✅ **Testeable**: Los tests verifican `IsSuccess`/`IsFailure` claramente
- ✅ **Type-safe**: El tipo de error está incrustado en el Result

### 💡 Conclusión

> El patrón **Result** con **CSharpFunctionalExtensions** proporciona una forma elegante de manejar errores sin excepciones, mejorando la testabilidad y legibilidad del código.

## 🎯 Cuándo Usar Mocks

| Caso | Usar Mocks | No Usar Mocks |
|------|-----------|---------------|
| Tests de unidad del servicio | ✅ | |
| Tests de lógica de negocio | ✅ | |
| Tests de repositorio | | ✅ |
| Tests de integración | | ✅ |
| Tests de base de datos | | ✅ |
| Tests de API externa | ✅ (si es costosa) | |

## 📦 Paquetes Instalados

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
<PackageReference Include="NUnit" Version="4.2.2" />
<PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />
<PackageReference Include="FluentAssertions" Version="6.12.2" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="CSharpFunctionalExtensions" Version="3.7.0" />
```

## 🛠️ Requisitos

- .NET 10.0+
- Visual Studio 2022 o JetBrains Rider

---

**recordatorio: Los mejores tests son los:**
1. ✅ Son rápidos
2. ✅ Son independientes
3. ✅ Son repetibles
4. ✅ Son auto-validantes
5. ✅ Se escriben a tiempo (TDD)