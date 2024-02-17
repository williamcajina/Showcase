#include "FileTranslator.h"
#include <maya/MGlobal.h>
#include <maya/MSelectionList.h>
#include <maya/MDagPath.h>
#include <maya/MFnTransform.h>
//simply returns a new copy of ourselves for Maya to use as our plug in
void * CFileTranslator::creator(void)
{
	return new CFileTranslator;
}

//this is the function we will need to complete to actually export our model
MStatus CFileTranslator::writer(const MFileObject& file, const MString& optionString, FileAccessMode mode)
{
	//this function is the one that gets called when you actually export
	//make sure to call all relevant functions from the CStaticMeshExporter class
	const char* path = file.rawPath().asChar();
	m_MeshExporter.SetExportDirectory(path);
		
	m_MeshExporter.ClearAndReset();


	bool exportAll = true;

	//TODO: determine if we should export all here and set exportAll bool appropriately
	//In writer() function, mode == kExportActiveAccessMode ?
	//	(True if “Export Selection…” was used.)

	if (mode == kExportActiveAccessMode)
		exportAll = false;
	else
		exportAll = true;
	
	

	m_MeshExporter.ExportMeshes( exportAll );



	m_MeshExporter.WriteOutMeshes();
		

	//return success
	return MStatus::kSuccess;
}