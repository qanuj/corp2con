app.controller('editOrCreateJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {

    $scope.title = "Edit Job";
    $scope.record = {};

    if (param.id) {
        db.job.get(param.id).success(function (result) {
            result.start = moment(result.start).toDate();
            result.end = moment(result.end).toDate();
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
    $scope.loadLocations = db.system.getLocations;

    $scope.save = function (record) {

        for (var x in record.secondarySkills) {
            record.secondarySkills[x].level = 'Secondary';
        }

        record.skills = record.primarySkills.concat(record.secondarySkills);
        

        if (param.id) {
            db.job.update(record)
            .success(function (result) {
                window.location = "#/jobs";
            });
        } else {
            db.job.create(record)
            .success(function (result) {
                window.location = "#/jobs";
            });
        }
    }
}]);