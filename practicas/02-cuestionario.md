# Preguntas de Investigación y Desarrollo (I+D)

**UD07 - Calidad de Software y Pruebas de Software**

---

**1. Reflexión sobre la pirámide de pruebas y la estrategia de testing:**
Analiza por qué la pirámide de pruebas (70% unitarias, 20% integración, 10% E2E) es una estrategia efectiva. ¿Qué problemas surgirían si inviertiéramos esta pirámide? Justifica tu respuesta con ejemplos de escenarios donde muchas pruebas E2E ralentizarían el desarrollo.

---

**2. Análisis del patrón AAA en pruebas complejas:**
En pruebas de servicios que dependen de múltiples repositorios y servicios externos, ¿cómo se aplica el patrón AAA? Investiga cómo el "Arrange" puede volverse complejo y qué estrategias existen para mantener los tests legibles y mantenibles.

---

**3. Impacto de la Complejidad Ciclomática en la mantenibilidad:**
La complejidad ciclomática de McCabe mide el número de rutas independientes en un código. Investiga cómo un valor alto de complejidad (por ejemplo, > 10) afecta a la capacidad de probar y mantener el código. ¿Cómo se relaciona esto con los principios SOLID?

---

**4. Comparación entre TDD y BDD en proyectos reales:**
Investiga y compara TDD (Test Driven Development) y BDD (Behavior Driven Development). ¿En qué tipo de proyectos es más apropiado cada enfoque? ¿Cómo afectan a la comunicación entre el equipo técnico y los stakeholders?

---

**5. Análisis de la relación entre Code Smells y Testing:**
Los code smells indican problemas en el código que pueden llevar a bugs. Investiga cómo los principios de Clean Code y las técnicas de refactorización se relacionan con la capacidad de escribir tests efectivos. ¿Puede un código con muchos code smells ser correctamente testeado?

---

**6. Impacto de la inyección de dependencias en la testabilidad:**
Investiga cómo la inyección de dependencias (DI) facilita el uso de mocks y stubs. ¿Por qué es difícil testear código que crea sus propias dependencias con `new`? Analiza el patrón Service Locator como alternativa a DI.

---

**7. Análisis de estrategias de mock: London School vs Classical TDD:**
Existen dos escuelas principales de TDD: la London School (mockist) que usa mocks para todo, y la Classical School que prefiere objetos reales cuando es posible. Investiga las ventajas y desventajas de cada enfoque y en qué escenarios es preferible cada uno.

---

**8. Cobertura de código: métricas vs calidad de tests:**
La cobertura de código mide qué porcentaje del código es ejecutado por tests, pero un 100% de cobertura no garantiza tests de calidad. Investiga qué otras métricas y prácticas complementan la cobertura para garantizar tests efectivos. ¿Cómo evitar el "test pollution"?

---

**9. Análisis de Serilog como sistema de logging estructurado:**
Investiga las ventajas del logging estructurado de Serilog frente al logging tradicional con `Console.WriteLine`. ¿Cómo facilita el uso de `Log.ForContext<T>()` y el `SourceContext` el diagnóstico de problemas en producción?

---

**10. Patrones de logging en arquitecturas distribuidas:**
En microservicios o aplicaciones distribuidas, el logging se vuelve crítico. Investiga cómo usar correlación de IDs (correlation IDs) para rastrear requests a través de múltiples servicios. ¿Cómo afecta esto a la configuración de Serilog?

---

**11. Análisis de herramientas de análisis estático:**
Investiga cómo SonarLint, SonarQube y StyleCop se complementan en un flujo de trabajo de desarrollo. ¿En qué se diferencian? ¿Puede una herramienta reemplazar a otra? ¿Cómo se integran en CI/CD?

---

**12. Testing de código asíncrono y manejo de excepciones:**
Investiga los desafíos de testear código asíncrono en C#. ¿Cómo afectan `async/await` y las excepciones asíncronas a la verificación con mocks? Analiza las mejores prácticas para testear servicios que consumen APIs externas.

---

**13. Análisis de técnicas de caja negra en el diseño de tests:**
El Particionamiento de Equivalencia (EP) y el Análisis de Valores Límite (BVA) son técnicas para diseñar casos de prueba desde los requisitos. Investiga cómo estas técnicas se aplican en metodologías ágiles donde los requisitos evolucionan constantemente.

---

**14. Reflexión sobre la documentación de tests:**
Los tests deben ser documentación viviente del código. Investiga qué prácticas hacen que los tests sean auto-documentados. ¿Cómo afecta el uso de FluentAssertions a la legibilidad de los tests comparado con Assert tradicionales?

---

**15. Análisis de Test Doubles y el teorema de Wiston:**
El teorema de Wiston establece que "mocks revelan el diseño". Investiga qué significa esto en la práctica. ¿Cómo el uso de mocks puede mejorar o empeorar el diseño de nuestro código?
