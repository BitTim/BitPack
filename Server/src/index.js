const express = require("express");
const app = express();

const constants = require("./constants")
const util = require("./util")
const loader = require("./loader")

var packageList = util.init(express, app, loader);

app.get("/packages", (req, res) => {
    res.status(200).send(packageList);
});

app.get("/packages/:id", (req, res) => {
    const { id } = req.params;

    if(!util.packageExists(packageList, id))
    {
        res.status(404).send("Package " + id + " not found");
        return;
    }

    res.status(200).send(loader.loadPackage(id));
});

app.get("/packages/:id/:version", (req, res) => {
    const { id, version } = req.params;
    
    if(!util.packageExists(packageList, id))
    {
        res.status(404).send("Package " + id + " not found");
        return;
    }

    if(!util.versionExists(id, version))
    {
        res.status(404).send("Package " + id + " does not contain the version " + version);
        return;
    }

    const versionData = loader.loadPackage(id).versions;
    res.status(200).send(versionData.filter((v) => v.version == version)[0]);
});

app.get("/packages/:id/:version/download", (req, res) => {
    const { id, version } = req.params;
    
    if(!util.packageExists(packageList, id))
    {
        res.status(404).send("Package " + id + " not found");
        return;
    }

    if(!util.versionExists(id, version))
    {
        res.status(404).send("Package " + id + " does not contain the version " + version);
        return;
    }

    const versionData = loader.loadPackage(id).versions;
    const typeData = versionData.filter((v) => v.version == version)[0].downloadTypes;
    res.status(200).send(typeData);
});

app.get("/packages/:id/:version/download/:type", (req, res) => {
    const { id, version, type } = req.params;
    
    if(!util.packageExists(packageList, id))
    {
        res.status(404).send("Package " + id + " not found");
        return;
    }

    if(!util.versionExists(id, version))
    {
        res.status(404).send("Package " + id + " does not contain the version " + version);
        return;
    }

    if(!util.typeExists(id, version, type))
    {
        res.status(404).send("Package " + id + "-" + version + " does not contain the download type " + type);
        return;
    }

    const filename = id + "-" + version + "_" + type + ".zip";
    const path = constants.packagePath + id + "/versions/" + version + "/" + filename;
    
    res.status(200).sendFile(path, { root: "./" });
});

//TODO: Calls to create and modify packages, but only with valid token

app.listen(constants.PORT, () => {
    console.log("Started on http://localhost:8080");
});