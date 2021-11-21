const express = require("express");
const app = express();

const constants = require("./constants")
const util = require("./util")

var packageList = util.init(express, app);

app.get("/packages", (req, res) => {
    res.status(200).send(packageList);
});

app.listen(constants.PORT, () => {
    console.log("Started on http://localhost:8080");
});