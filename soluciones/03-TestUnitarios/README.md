# TestUnitarios - Proyecto de Pruebas Unitarias

## 📚 Objetivo del Proyecto

Este proyecto demuestra los conceptos fundamentales de las **pruebas unitarias** en C# utilizando:
- **NUnit** como framework de testing
- **FluentAssertions** para aserciones fluidas
- **Patrón AAA** (Arrange-Act-Assert)
- Estructura con clases inner para casos válidos e inválidos

## 🏗️ Estructura del Proyecto

```
TestUnitarios/
├── TestUnitarios/                    # Proyecto principal (código a testear)
│   ├── Models/
│   │   └── Persona.cs               # Record con validación de calificación
│   ├── Services/
│   │   └── Calculadora.cs           # Calculadora con operaciones básicas
│   ├── Validators/
│   │   └── PersonaValidator.cs      # Validador de Persona
│   └── Program.cs                   # Entry point
│
└── TestUnitarios.Test/               # Proyecto de tests
    ├── Inicial/
    │   └── ClaseEjemplo.cs          # Ejemplos de atributos NUnit
    ├── Services/
    │   ├── CalculadoraAsserts.cs    # Tests con NUnit assertions
    │   └── CalculadoraFluent.cs     # Tests con FluentAssertions
    ├── Validators/
    │   └── PersonaValidatorTests.cs # Tests del validador
    └── Models/
        └── PersonaTests.cs          # Tests de Persona (calificación)
```

## 📖 Conceptos Aprendidos

### 1. Atributos de NUnit

| Atributo | Descripción |
|----------|-------------|
| `[TestFixture]` | Marca una clase que contiene tests |
| `[Test]` | Marca un método como caso de prueba |
| `[SetUp]` | Se ejecuta antes de cada test |
| `[TearDown]` | Se ejecuta después de cada test |
| `[OneTimeSetUp]` | Se ejecuta una vez al inicio (recursos compartidos) |
| `[OneTimeTearDown]` | Se ejecuta una vez al final |
| `[TestCase]` | Define casos de prueba parametrizados |
| `[TestCaseSource]` | Define casos desde fuente externa |
| `[Ignore]` | Ignora un test |
| `[Property]` | Añade metadatos al test |

### 2. Aserciones

**Con NUnit (Assert.That):**
```csharp
Assert.That(resultado, Is.EqualTo(5));
Assert.That(lista, Does.Contain(2));
Assert.Throws<DivideByZeroException>(() => calc.Dividir(10, 0));
```

**Con FluentAssertions (Should):**
```csharp
resultado.Should().Be(5);
lista.Should().Contain(2);
accion.Should().Throw<DivideByZeroException>();
```

### 3. Patrón AAA

```csharp
[Test]
public void Sumar_DosNumeros_RetornaSuma()
{
    // Arrange - Preparar el escenario
    var calculadora = new Calculadora();
    int a = 5, b = 3;

    // Act - Ejecutar la acción
    int resultado = calculadora.Sumar(a, b);

    // Assert - Verificar el resultado
    resultado.Should().Be(8);
}
```

### 4. Estructura de Tests

```csharp
[TestFixture]
public class CalculadoraTests
{
    private Calculadora _calculadora;

    [SetUp]
    public void SetUp() => _calculadora = new Calculadora();

    // Inner class para casos válidos
    [TestFixture]
    public class CasosValidos { }

    // Inner class para casos inválidos
    [TestFixture]
    public class CasosInvalidos { }
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

### Ejecutar un proyecto específico:
```bash
dotnet test TestUnitarios.Test/TestUnitarios.Test.csproj
```

## 📊 Cobertura de Código

### Resultados Actuales:
- **Líneas cubiertas:** 115 / 120 (95.8%)
- **Ramas cubiertas:** 59 / 64 (92.1%)

### ¿Qué es la Cobertura de Código?

La cobertura de código mide qué porcentaje del código fuente es ejecutado durante las pruebas. Nos ayuda a identificar:
- Código sin probar
- Ramas no ejecutadas
- Casos no cubiertos

### 🚨 Importante: Caja Blanca y Caja Negra

La cobertura de código nos ayuda a identificar **casos que no hemos probado**, pero **no nos dice si los casos que probamos son los correctos**.

Por eso，debemos basarnos en las técnicas aprendidas:

#### 🔲 Caja Negra (Desde los requisitos)
- **Particiones de Equivalencia (EP):** Dividir el dominio de entrada en clases equivalentes
- **Análisis de Valores Límite (BVA):** Probar los extremos de cada partición
- **Tablas de Decisión:** Para combinaciones de condiciones

Ejemplo para la Calculadora:
- Partición válido: números reales (representantes: 2, 10, -5)
- Partición inválido: división por cero

#### 🔲 Caja Blanca (Desde el código)
- **Complejidad Ciclomática:** Número de caminos independientes
- **Prueba de Ramas:** Cada rama (if/else) debe probarse
- **Prueba de Caminos:** Cada camino independiente

### 💡 Cómo Usar la Cobertura para Mejorar

1. **Ejecutar tests con cobertura:**
   ```bash
   dotnet test --collect:"XPlat Code Coverage"
   ```

2. **Ver informe HTML:**
   Lo primero que debemos hacer es instalar ReportGenerator para convertir el XML de cobertura a un formato legible:
   ```bash
   dotnet tool install -g dotnet-reportgenerator-globaltool
   ```
Luego ejecutamos el siguiente comando para generar el informe HTML:
   ```bash
   reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
   ```

3. **Abrir el informe en navegador:**
   ```bash
   start coveragereport/index.html   # Windows
   open coveragereport/index.html    # macOS
   xdg-open coveragereport/index.html # Linux
   ```

4. **Analizar líneas no cubiertas:**
   - Las líneas en ROJO no se ejecutaron
   - Las líneas en AMARILLO son ramas no ejecutadas completamente
   - Añadir tests para cubrir esos casos

5. **Vincular con técnicas de pruebas:**
   - Si hay código sin cubrir, aplicar **Caja Negra** (desde requisitos)
   - Si hay ramas/condiciones sin probar, aplicar **Caja Blanca** (desde código)

## 🎯 Conclusión

> La cobertura de código es una herramienta de ayuda, no un objetivo en sí mismo. Un 80% bien elegido es mejor que un 100% con tests mal diseñados.

** recordatorio: Los tests de alta calidad cumplen los principios F.I.R.S.T.:**
- **F**ast (Rápidos)
- **I**ndependent (Independientes)
- **R**epeatable (Repetibles)
- **S**elf-Validating (Auto-validados)
- **T**imely (Oportunos)

---

## 📦 Paquetes Instalados

```xml
<!-- TestUnitarios.Test.csproj -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
<PackageReference Include="NUnit" Version="4.2.2" />
<PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />
<PackageReference Include="FluentAssertions" Version="6.12.2" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
```

## 🛠️ Requisitos

- .NET 10.0+
- Visual Studio 2022 o JetBrains Rider