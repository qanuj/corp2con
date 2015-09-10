app.controller('companyEditOrCreateJobController', ['$scope', 'dataService', '$stateParams', '$window', '$rootScope', function ($scope, db, param, $window, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Job";
    $scope.subtitle = "";
    $scope.record = {};

    if (param.id) {
        $scope.title="Edit Job";
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
        record.start = record.date.startDate;
        record.end = record.date.endDate;

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