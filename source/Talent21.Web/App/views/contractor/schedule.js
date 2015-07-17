app.controller('jobscheduleController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        db.contractor.createSchedule(record).success(refreshRecord);
    }

    function refreshRecord() {
        return db.contractor.getSchedule().success(function (result) {
            angular.forEach(result, function (d) {
                d.start = new Date(d.start);
                d.end = new Date(d.end);
            });
            $scope.schedule = result;
        });
    }

    $scope.update = function (s) {
        db.contractor.editSchedule(s).success(refreshRecord);
    };

    $scope.delete = function (s) {
        db.contractor.deleteSchedule(s).success(refreshRecord);
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };

    refreshRecord();
}]);