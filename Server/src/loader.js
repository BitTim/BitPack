const fs = require("fs");
const constants = require("./constants")

// Utility funtion to load JSON files
function loadJSON(path)
{
    var data = JSON.parse(fs.readFileSync(path));
    return data;
}

// Function to scan all Packages for a manifest file and add the packageID, name and latest version to packagelist.json
exports.generatePackageList = () =>
{
    var data = {};
    var files = fs.readdirSync(constants.packagePath)

    // Iterate over files
    files.forEach(f => {
        var stat = fs.statSync(constants.packagePath + f)
        if(!stat.isDirectory()) return;

        // Read from Manifest
        var manifest = loadJSON(constants.packagePath + f + "/manifest.json");

        // Iterate over channels
        constants.channels.forEach(c => {
            if(data[c] == undefined) data[c] = [];

            // Get latest version for specified channel
            var channelVersion = manifest.channels[c];
            if(channelVersion == undefined) channelVersion = manifest.versions[manifest.versions.length - 1].version; // Use latest if none specified

            // Push package data onto return value
            data[c].push({
                id: f,
                latest: channelVersion
            });
        })
    });

    // Backup and Overwrite packagelist.json
    fs.copyFileSync(constants.packagelistPath, constants.packagelistBackupPath);
    fs.writeFileSync(constants.packagelistPath, JSON.stringify(data, null, "\t"));

    return data;
}

// Function to return the manifest file for the specified package
exports.loadPackage = (packageID) =>
{
    var path = constants.packagePath + packageID + "/manifest.json";
    var data = loadJSON(path);
    return data;
}