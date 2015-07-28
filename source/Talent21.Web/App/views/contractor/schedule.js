app.controller('jobscheduleController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
    $('input[type=text]').each(function () {
            $(this).val('');
        });
        db.contractor.createSchedule(record).success(refreshRecord);
    }

    function refreshRecord(page) {
        return db.contractor.getSchedule(page).success(function (result) {
            angular.forEach(result, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.schedule = result;
            angular.element('#startdate').val("");
            angular.element('#enddate').val("");

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