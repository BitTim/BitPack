const loader = require("./loader");

// Initialize server
exports.init = (express, app, loader) =>
{
    app.use(express.json())

    var packagelist = loader.generatePackageList();
    return packagelist;
}

// Check if package exists
exports.packageExists = (packageList, id) =>
{
    var found = false;

    for(var channel in packageList)
    {
        var filtred = packageList[channel].filter(p => p.id == id);
        if(filtred.length > 0)
        {
            found = true;
            break;
        }
    }

    return found;
}

// Check if version of package exists
exports.versionExists = (id, version) =>
{
    const versionData = loader.loadPackage(id).versions;
    if(versionData.filter((v) => v.version == version).length > 0) return true;
    else return false;
}

// Check if download type of version exists
exports.typeExists = (id, version, type) =>
{
    const versionData = loader.loadPackage(id).versions;
    const fetchedVersion = versionData.filter((v) => v.version == version)[0];
    if(fetchedVersion.downloadTypes.indexOf(type) != -1) return true;
    else return false;
}