app.controller('editOrCreateJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {

    $scope.title = "Edit Job";
    $scope.record = {};

    if (param.id) {
        db.job.get(param.id).success(function (result) {
            result.loc = { formatted_address: result.location };
            result.primarySkills = [];
            result.secondarySkills = [];
            for (var x in result.skills) {
                if (result.skills[x].level == 'Primary') {
                    result.primarySkills.push(result.skills[x]);
                } else {
                    result.secondarySkills.push(result.skills[x]);
                }
            }
            $scope.record = result;
        });
    }

    $scope.loadSkills = db.system.getSkills;

    $scope.refreshAddresses = function (address) {
        return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    $scope.save = function (record) {

        if (record.loc) {
            record.location = record.loc.formatted_address;
        }
        record.skills = record.primarySkills.concat(record.secondarySkills);


        if (param.id) {
            db.job.update(record)
            .success(function (result) {
                window.location = "#/profile";
            });
        } else {
            db.job.create(record)
            .success(function (result) {
                window.location = "#/profile";
            });
        }
    }
}]);