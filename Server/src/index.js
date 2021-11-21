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

app.listen(constants.PORT, () => {
    console.log("Started on http://localhost:8080");
});