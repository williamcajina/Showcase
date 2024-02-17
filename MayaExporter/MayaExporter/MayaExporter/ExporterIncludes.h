#pragma once
#include <string>
#include <vector>

struct tVertex 
{
	float fX, fY, fZ; //Coordinates
	float fNX, fNY, fNZ; // Normals
	float fU, fV; // Texture coordinates
	
	bool operator==(const tVertex &other) const
	{
		if (this->fX != other.fX)
			return false;
		if (this->fY != other.fY)
			return false;
		if (this->fZ != other.fZ)
			return false;
		if (this->fNX != other.fNX)
			return false;
		if (this->fNY != other.fNY)
			return false;
		if (this->fNZ != other.fNZ)
			return false;
		if (this->fU != other.fU)
			return false;
		if (this->fV != other.fV)
			return false;

		return true;

	}
};
struct tTriangle
{
	unsigned int uIndices[3];
};

class CMesh 
{
public:
	std::string 			m_strName; 		// This is the name of the mesh.
	std::vector<std::string> 	m_vTextureNames;	// These are the textures that are used in this mesh.
	std::vector<tVertex>  	m_vUniqueVerts;	// These are all the unique vertices in this mesh.
	std::vector<tTriangle> 	m_vTriangles;		// These are the triangles that make up the mesh.    

};


	

