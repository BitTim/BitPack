const loader = require("./loader");
const constants = require("./constants")

exports.init = (express, app) =>
{
    app.use(express.json())

    var packagelist = loader.generatePackageList();
    return packagelist;
}