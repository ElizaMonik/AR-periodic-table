# 🧪 Sistema de Realidad Aumentada para Enseñar la Tabla Periódica

Este repositorio contiene todo el proyecto para aprender química de forma **interactiva** usando **cubos 3D impresos** y **realidad aumentada (AR)**.  
El sistema muestra **átomos y moléculas en 3D**, combinando **Unity**, **Vuforia** y una **API en la nube**.

---

## 🚀 ¿Qué hace este proyecto?

- Reconoce **cubos físicos con marcadores QR** usando la cámara del celular.
- Muestra **modelos 3D de 118 elementos químicos**.
- Permite **combinar átomos** para formar moléculas simples.
- Descarga modelos 3D desde una **API FastAPI** alojada en **Google Cloud**, para que la app no pese tanto.
- Incluye una **interfaz fácil de usar** con botones para explorar la tabla periódica, iniciar la cámara, ver información y más.

---

## 📁 Estructura del repositorio

📦 /UnityProject/ → Proyecto completo en Unity (escenas, scripts)
📦 /BlenderModels/ → Modelos 3D de átomos (.blend, .glb)
📦 /FastAPI/ → Código de la API que sirve los modelos y metadatos
📦 /Docs/ → Manuales, diagramas y capturas de pantalla
📄 README.md → Este archivo

yaml
Copiar
Editar

---

## 🛠️ Requisitos

- **Unity** (versión 2021 o superior)
- **Vuforia Engine**
- **Python 3.9+** (para correr FastAPI)
- **Google Cloud** (opcional para desplegar la API)
- **Ultimaker Cura** (opcional para imprimir los cubos)

---

## ⚙️ Cómo usarlo paso a paso

1️⃣ **Clona este repositorio**

```bash
git clone https://github.com/tu-usuario/tu-repo.git
2️⃣ Ejecuta la API

bash
Copiar
Editar
cd FastAPI
uvicorn main:app --reload
3️⃣ Abre el proyecto Unity

Abre /UnityProject/ con Unity Hub.

Busca la escena principal MainScene.unity.

4️⃣ Imprime tus cubos

Los archivos STL están en /BlenderModels/.

Puedes imprimirlos huecos para ahorrar material.

Cada cara tiene un marcador QR en relieve.

5️⃣ Prueba la aplicación

Enciende la cámara RA.

Escanea un cubo físico.

El modelo 3D se carga automáticamente desde la nube.

Si escaneas 2 cubos compatibles, ¡aparece una molécula combinada!

👀 Ejemplo visual
📸 Así se ve el sistema en acción:


🔗 Funciones clave
Botón	Qué hace
Explorar Tabla	Muestra todos los elementos de la tabla periódica.
Iniciar RA	Activa la cámara para escanear cubos.
Fusiones Moleculares	Abre lista de combinaciones posibles y activa la cámara.
Información	Explica cómo funciona y para qué sirve la app.
Salir	Cierra la aplicación.

📚 Documentación extra
Revisa la carpeta /Docs/ para encontrar:

Manual de usuario 📖

Guía de impresión 3D 🧊

Cómo usar la API 🔗
