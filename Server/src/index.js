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
    res.status(200).send(loader.loadPackage(id));
});

app.get("/packages/:id/:version", (req, res) => {
    const { id, version } = req.params;
    
    const versionData = loader.loadPackage(id).versions;
    res.status(200).send(versionData.filter((v) => v.version == version)[0]);
});

app.get("/packages/:id/:version/:type", (req, res) => {
    const { id, version, type } = req.params;
    
    const filename = id + "-" + version + "_" + type + ".zip";
    const path = constants.packagePath + id + "/" + version + "/" + filename;
    
    res.status(200).sendFile(path, { root: "./" });
});



app.listen(constants.PORT, () => {
    console.log("Started on http://localhost:8080");
});