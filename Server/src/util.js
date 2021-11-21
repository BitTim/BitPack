const loader = require("./loader");

exports.init = (express, app, loader) =>
{
    app.use(express.json())

    var packagelist = loader.generatePackageList();
    return packagelist;
}