app.controller('jobscheduleController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        db.contractor.createSchedule(record).success(refreshRecord).finally(function() {
            $scope.start = '';
            $scope.end = '';
        });
    }

    function refreshRecord(page) {
        return db.contractor.getSchedule(page).success(function (result) {
            angular.forEach(result, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
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