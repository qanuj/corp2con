app.controller('jobscheduleController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {

    $scope.navigate = function (page) {
        db.contractor.pagedSchedule(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            console.log($scope.records)
            });

    $scope.save = function (record) {
        db.contractor.createSchedule(record).success($scope.navigate()).finally(function () {
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
        db.contractor.editSchedule(s).success($scope.navigate(params.page));
    };

    $scope.delete = function (s) {
        db.contractor.deleteSchedule(s).success($scope.navigate(params.page));
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };
    }
    $scope.navigate(params.page);
    //refreshRecord();
}]);