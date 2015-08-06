app.controller('contractorApplicationController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {

    function loadSchedule(id) {
        return db.company.getSchedule(id).success(function (result) {
            angular.forEach(result, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.schedule = result;
        });
    }

    function loadOthers(jobId, contractorId) {

        db.system.enums('levelEnum').then(function (enums) {
            $scope.levels = enums;
        });

        loadSchedule(contractorId);

        db.contractor.get(contractorId).success(function (result) {
            $scope.record = result;
        });

        db.job.get(jobId).success(function (result) {
            $scope.job = result;
        });
    }

    db.company.getJobApplication(param.id).success(function (row) {
        $scope.application = row;
        loadOthers(row.job.id, row.contractor.id);
    });

}]);