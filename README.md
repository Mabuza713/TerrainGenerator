# Terrain Generation Unity Project

## Overview

This Unity project is designed to generate terrain using Perlin and Voronoi noise maps. The project includes scripts for generating noise maps, creating meshes based on these maps, and rendering the results within the Unity environment.

## Table of Contents

- [Project Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Usage](#usage)

## Features

- **Perlin Noise Map Generation**: Generate terrain heightmaps using Perlin noise.
- **Voronoi Noise Map Generation**: Create biome maps using Voronoi noise.
- **Mesh Generation**: Construct 3D meshes based on the noise maps.
- **Rendering**: Display the generated terrain and biomes with appropriate materials and textures.

## Project Structure

### 1. Display.cs

Responsible for rendering the noise maps and meshes.

- `DrawPerlinNoiseMap(float[,] noiseMap)`: Generates a texture from a Perlin noise map.
- `DrawVoronoiMap(Color[,] colorMap)`: Generates a texture from a Voronoi noise map.
- `DrawMesh(MeshData meshData, Color[,] colorMap)`: Constructs and renders a mesh based on height and biome data.

### 2. MapGenerator.cs

Handles the generation of the noise maps and coordinates rendering.

- `GenerateMap()`: Generates the appropriate map (Perlin, Voronoi, or Mesh) based on the selected `noiseType`.

### 3. MeshGenerator.cs

Static class responsible for generating mesh data.

- `GenerateTerrainMesh(float[,] heightMap, float meshHeightMultiplier)`: Creates mesh data from a heightmap.

### 4. MeshData.cs

Class representing the mesh data structure.

- `AddTriangle(int point1, int point2, int point3)`: Adds a triangle to the mesh.
- `GenerateMesh()`: Converts the mesh data into a Unity `Mesh` object.

### 5. NoiseGenerator.cs

Static class for generating noise maps.

- `GeneratePerlinNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity)`: Generates a Perlin noise map.
- `GenerateVoronoiNoiseMap(int width, int height, int biomesAmount)`: Generates a Voronoi noise map.
## Usage

1. Add simulation folder to unity

2. In hierarchy find MapGenerator

3. To change current terrain press `GENERATE`
