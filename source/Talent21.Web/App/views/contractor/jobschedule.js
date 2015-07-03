app.controller('jobscheduleController', ['$scope', 'dataService', function ($scope, db) {
    $scope.isCollapsed = false;
    $scope.save = function (record) {
        console.log(record)
        db.contractor.createSchedule(record).success(function (result) {
            console.log(result);
        });
    }
}]);
