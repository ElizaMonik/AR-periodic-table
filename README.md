# 🧪 Sistema de Realidad Aumentada con Cubos 3D para Enseñar Química

Este proyecto propone una solución educativa innovadora: **enseñar la tabla periódica y la formación de moléculas usando cubos físicos impresos en 3D combinados con realidad aumentada (RA)**.  
Diseñado para facilitar la comprensión de conceptos químicos abstractos, el sistema permite que los estudiantes interactúen con modelos 3D de átomos y moléculas en tiempo real, a través de una aplicación desarrollada en **Unity con Vuforia**, respaldada por una **API REST en FastAPI** y almacenamiento en la nube.

---

## 🚀 ¿Por qué este proyecto es diferente?

A diferencia de otras aplicaciones educativas, este sistema integra el mundo físico y digital mediante:

✅ **Cubos físicos impresos con marcadores en relieve** que representan elementos químicos.  
✅ **Reconocimiento visual inteligente** a través de la cámara del dispositivo móvil.  
✅ **Visualización en RA de modelos atómicos y moléculas animadas**.  
✅ **Carga dinámica desde una API en la nube** para reducir el peso de la aplicación.  
✅ **Interfaz intuitiva** que guía al usuario paso a paso.

> 📌 *Una herramienta educativa pensada no solo para aprender, sino para asombrar.*

---

## 🔍 Características destacadas

- Visualización de **los 118 elementos de la tabla periódica** con modelos 3D.  
- Fusión interactiva de elementos para **crear moléculas simples** (como H₂O, CO₂, NH₃).  
- Uso de **cubos 3D físicos con relieves únicos** como marcadores para RA.  
- Compatible con dispositivos Android.  
- Arquitectura flexible, escalable y basada en componentes.

---

## 🛠️ Herramientas y Tecnologías utilizadas

| Tecnología      | Uso Principal                                           |
|----------------|----------------------------------------------------------|
| Unity          | Motor principal del sistema interactivo                  |
| Vuforia        | Reconocimiento de marcadores en 3D (relieves físicos)    |
| FastAPI        | API REST para servir modelos y metadatos                 |
| Google Cloud   | Almacenamiento en la nube para modelos 3D `.glb`            |
| ChimeraX       | Visualización científica de moléculas                    |
| Ultimaker Cura | Preparación de impresión de cubos físicos                |

---

## 📁 Estructura del repositorio

```text
📦 UnityProject/     → Proyecto Unity con scripts, escenas y UI
📦 Models/           → Modelos 3D de átomos (.glb)
📦 FastAPI/          → Código fuente de la API REST
📦 Tinkercard/       → Archivos STL de los cubos impresos
📦 Docs/             → Manuales, guías y capturas
📄 README.md         → Este archivo
⚙️ Cómo iniciar el proyecto
1️⃣ Clonar el repositorio
bash
Copiar
Editar
git clone https://github.com/tu-usuario/tu-repo.git
2️⃣ Ejecutar la API REST
bash
Copiar
Editar
cd FastAPI
uvicorn main:app --reload
3️⃣ Abrir Unity y cargar la escena principal
Abrir /UnityProject/ con Unity Hub

Ejecutar MainScene.unity

4️⃣ Probar en dispositivo Android
Instalar el APK (ubicado en /Docs/ o generado desde Unity).

Apuntar la cámara hacia un cubo impreso.

Visualizar el modelo atómico en RA.

Al combinar dos cubos compatibles, se genera una molécula animada.

🧊 Impresión de cubos físicos
Los cubos fueron diseñados con relieves personalizados en cada cara, lo que permite a la cámara reconocerlos como marcadores únicos.
La impresión se puede realizar en PLA con impresora 3D, recomendando una impreso de alta calidad, usando los archivos .stl ubicados en la carpeta /Tinkercard/.

💬 Conclusión
Este proyecto representa un verdadero avance en la integración de tecnologías emergentes para la educación científica.
La unión de RA + impresión 3D + modelado molecular ha resultado en una experiencia educativa inmersiva, atractiva y altamente eficaz.

Un ingeniero que vea este sistema comprenderá de inmediato su valor pedagógico, su solidez técnica y su potencial de impacto en aulas reales.
