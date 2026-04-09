### Cuestionario de Testing y Calidad de Software

**UD07 - Calidad de Software y Pruebas de Software**

---

**1. ¿Qué aspecto del software verifican las pruebas unitarias?**
A) El rendimiento de la base de datos.
B) El comportamiento de unidades individuales de código de forma aislada.
C) La interfaz gráfica de usuario.
D) El rendimiento de la red.

**2. ¿Cuál de los siguientes NO es un principio F.I.R.S.T.?**
A) Fast.
B) Independent.
C) Repeatable.
D) Extensive.

**3. En el principio "Independent" de F.I.R.S.T., ¿qué significa que los tests sean independientes?**
A) Los tests deben ejecutarse en orden específico.
B) Los tests no deben depender unos de otros.
C) Los tests deben compartir estado entre ellos.
D) Los tests deben usar la misma base de datos.

**4. ¿Qué representa la fase "Arrange" en el patrón AAA?**
A) Verificar el resultado esperado.
B) Preparar el escenario y configurar objetos.
C) Ejecutar la acción bajo prueba.
D) Limpiar recursos después del test.

**5. ¿Qué representa la fase "Act" en el patrón AAA?**
A) Preparar el escenario.
B) Verificar el resultado.
C) Ejecutar la acción bajo prueba.
D) Crear los mocks.

**6. ¿Qué representa la fase "Assert" en el patrón AAA?**
A) Preparar el escenario.
B) Ejecutar la acción.
C) Verificar que el resultado es el esperado.
D) Crear los mocks.

**7. ¿Qué es NUnit?**
A) Un framework de mocking.
B) Un framework de testing para .NET.
C) Una herramienta de cobertura de código.
D) Un sistema de logging.

**8. ¿Qué atributo marca una clase como contenedor de tests en NUnit?**
A) `[Test]`
B) `[TestFixture]`
C) `[SetUp]`
D) `[OneTimeSetUp]`

**9. ¿Qué atributo marca un método como test en NUnit?**
A) `[TestFixture]`
B) `[TestCase]`
C) `[Test]`
D) `[TestMethod]`

**10. ¿Qué diferencia hay entre `[SetUp]` y `[OneTimeSetUp]` en NUnit?**
A) No hay diferencia.
B) `[SetUp]` se ejecuta una vez, `[OneTimeSetUp]` antes de cada test.
C) `[SetUp]` se ejecuta antes de cada test, `[OneTimeSetUp]` una sola vez.
D) `[OneTimeSetUp]` se ejecuta después de cada test.

**11. ¿Qué es FluentAssertions?**
A) Un framework de mocking.
B) Una librería de aserciones con sintaxis fluida.
C) Un sistema de logging.
D) Una herramienta de cobertura.

**12. ¿Cuál es la sintaxis de FluentAssertions para verificar igualdad?**
A) `Assert.That(x, Is.EqualTo(5))`
B) `x.Should().Be(5)`
C) `Assert.AreEqual(x, 5)`
D) `x.Should().Equal(5)`

**13. ¿Qué es Moq?**
A) Un framework de testing.
B) Una librería de aserciones.
C) Una librería de mocking para .NET.
D) Un sistema de logging.

**14. ¿Cómo se crea un mock en Moq?**
A) `var mock = new Mock<IMiInterfaz>();`
B) `var mock = Mock.Create<IMiInterfaz>();`
C) `var mock = new IMiInterfaz();`
D) `var mock = CreateMock<IMiInterfaz>();`

**15. ¿Para qué sirve el método `Setup` en Moq?**
A) Para verificar que un método fue llamado.
B) Para configurar el comportamiento del mock.
C) Para crear el mock.
D) Para limpiar el mock.

**16. ¿Para qué sirve el método `Verify` en Moq?**
A) Para configurar el mock.
B) Para verificar que se cumplieron las expectativas.
C) Para crear el mock.
D) Para retornar valores.

**17. ¿Qué es un Test Double de tipo "Stub"?**
A) Un objeto que reemplaza una dependencia y verifica llamadas.
B) Un objeto que proporciona respuestas predefinidas a llamadas.
C) Un objeto que solo cumple requisitos de parámetros.
D) Un objeto con implementación funcional simplificada.

**18. ¿Qué es un Test Double de tipo "Dummy"?**
A) Un objeto que reemplaza una dependencia real.
B) Un objeto que registra llamadas.
C) Un objeto pasado pero nunca usado realmente.
D) Un objeto que verifica comportamiento.

**19. ¿Qué es un Test Double de tipo "Mock"?**
A) Un objeto que proporciona respuestas predefinidas.
B) Un objeto con implementación simplificada.
C) Un objeto pre-programado con expectativas que verifica llamadas.
D) Un objeto que solo se usa para llenar parámetros.

**20. ¿Qué es un Test Double de tipo "Fake"?**
A) Un objeto que verifica llamadas.
B) Un objeto con implementación simplificada pero funcional.
C) Un objeto que proporciona respuestas predefinidas.
D) Un objeto que no se usa.

**21. ¿Qué tipo de Test Double es un Spy?**
A) Un objeto que proporciona respuestas predefinidas.
B) Un objeto que registra información sobre cómo fue llamado.
C) Un objeto con implementación simplificada.
D) Un objeto que no se usa.

**22. ¿Qué es el Análisis de Valores Límite (BVA)?**
A) Una técnica que prueba los valores máximos de memoria.
B) Una técnica que prueba los valores en los bordes de las particiones.
C) Una técnica que prueba todos los valores posibles.
D) Una técnica que prueba valores aleatorios.

**23. ¿Qué es el Particionamiento de Equivalencia (EP)?**
A) Dividir el dominio en grupos donde el comportamiento es igual.
B) Probar todos los valores posibles.
C) Probar solo valores límite.
D) Probar valores aleatorios.

**24. ¿Qué es la Complejidad Ciclomática de McCabe?**
A) Una métrica para medir la velocidad del código.
B) Una métrica para medir la complejidad basada en ramas.
C) Una métrica para medir la cobertura de tests.
D) Una métrica para medir el tamaño del código.

**25. ¿Qué representa la base de la pirámide de pruebas (70%)?**
A) Pruebas E2E.
B) Pruebas de integración.
C) Pruebas unitarias.
D) Pruebas de rendimiento.

**26. ¿Qué representa la parte superior de la pirámide (10%)?**
A) Pruebas unitarias.
B) Pruebas de integración.
C) Pruebas E2E.
D) Pruebas de rendimiento.

**27. ¿Qué es Serilog?**
A) Un framework de testing.
B) Una librería de mocking.
C) Una librería de logging para .NET.
D) Una herramienta de cobertura.

**28. ¿Qué sink de Serilog se usa para escribir en consola?**
A) `Serilog.Sinks.File`
B) `Serilog.Sinks.Console`
C) `Serilog.Sinks.Database`
D) `Serilog.Sinks.Email`

**29. ¿Qué es `Log.ForContext<T>()` en Serilog?**
A) Crea un logger sin contexto.
B) Crea un logger con información de contexto (SourceContext).
C) Crea un logger de archivo.
D) Crea un logger global.

**30. ¿Qué es Coverlet?**
A) Un framework de testing.
B) Una librería de mocking.
C) Una herramienta de cobertura de código para .NET.
D) Un sistema de logging.

**31. ¿Qué comando se usa para ejecutar tests con cobertura en .NET?**
A) `dotnet run --coverage`
B) `dotnet test --collect:"XPlat Code Coverage"`
C) `dotnet build --coverage`
D) `dotnet cover`

**32. ¿Qué es ReportGenerator?**
A) Un framework de testing.
B) Una herramienta para generar informes HTML desde datos de cobertura.
C) Un sistema de logging.
D) Una librería de mocking.

**33. ¿Cuál es un buen objetivo de cobertura de código?**
A) 100%.
B) 50%.
C) Al menos 80%.
D) 0%.

**34. ¿Qué tipo de cobertura mide el porcentaje de ramas (if/else) ejecutadas?**
A) Line Coverage.
B) Branch Coverage.
C) Method Coverage.
D) Class Coverage.

**35. ¿Qué significa TDD?**
A) Test Driven Design.
B) Test Driven Development.
C) Testing Data Design.
D) Testing Development Database.

**36. En TDD, ¿en qué orden se escriben las cosas?**
A) Código de producción, luego tests, luego refactorización.
B) Tests, luego código de producción, luego refactorización.
C) Refactorización, luego código de producción, luego tests.
D) Código de producción y tests al mismo tiempo.

**37. ¿Qué es BDD?**
A) Bug Driven Development.
B) Behavior Driven Development.
C) Build Driven Design.
D) Basic Development Design.

**38. ¿Por qué la cobertura del 100% no garantiza ausencia de bugs?**
A) Porque los tests pueden estar mal escritos y solo verifican ejecución, no corrección.
B) Porque el código es muy simple.
C) Porque no hay tests suficientes.
D) Porque los tests son muy lentos.

**39. ¿Qué es SonarLint?**
A) Un plugin de IDE para análisis estático de código.
B) Un sistema de logging.
C) Una herramienta de testing.
D) Un framework de mocking.

**40. ¿Qué es StyleCop?**
A) Un framework de testing.
B) Un analizador de estilo de código para C#.
C) Una herramienta de logging.
D) Un sistema de build.

**41. ¿Qué son los "Code Smells"?**
A) Errores de compilación.
B) Indicadores de posibles problemas en el código.
C) Bugs de runtime.
D) Warnings del compilador.

**42. ¿Qué significa `restrictedToMinimumLevel` en Serilog?**
A) Configura el nivel mínimo de logging.
B) Configura el nivel máximo de logging.
C) Configura el formato de salida.
D) Configura el archivo de salida.

**43. ¿Qué significa `rollingInterval` en Serilog?**
A) El intervalo de tiempo entre logs.
B) La frecuencia con la que se crea un nuevo archivo de log.
C) El tamaño máximo del archivo.
D) El nivel de detalle del log.

**44. ¿Qué significa `retainedFileCountLimit` en Serilog?**
A) El número máximo de archivos de log a mantener.
B) El tamaño máximo de cada archivo.
C) El número de días que se guardan los logs.
D) El número máximo de líneas por archivo.

**45. ¿Qué es `[TestCase]` en NUnit?**
A) Para ignorar un test.
B) Para definir casos de prueba parametrizados.
C) Para ordenar tests.
D) Para agrupar tests.

**46. ¿Qué es `[Ignore]` en NUnit?**
A) Un atributo para marcar tests como importantes.
B) Un atributo para ignorar/ejecutar un test.
C) Un atributo para ordenar tests.
D) Un atributo para agrupar tests.

**47. ¿Qué devuelve `.And` en FluentAssertions?**
A) Un error.
B) Permite encadenar más aserciones.
C) Un valor booleano.
D) Un nuevo objeto.

**48. ¿Qué tipo de test double usarías para verificar que se llamó a un método?**
A) Dummy.
B) Stub.
C) Mock.
D) Fake.

**49. ¿Qué tipo de test double usarías para simular una base de datos en memoria?**
A) Dummy.
B) Stub.
C) Mock.
D) Fake.

**50. ¿Qué tipo de test double usarías simplemente para cumplir con un parámetro que nunca se usa?**
A) Dummy.
B) Stub.
C) Mock.
D) Fake.
