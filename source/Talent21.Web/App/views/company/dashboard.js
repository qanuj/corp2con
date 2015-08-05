app.controller('companyDashboardController', ['$scope','dataService', function ($scope,db) {
    db.company.dashboard().success(function (result) {
        result.aggregate.duration.min = moment(result.aggregate.duration.min).toDate();
        result.aggregate.duration.max = moment(result.aggregate.duration.max).toDate();
        console.log(result);
        $scope.record = result;
    });

    db.company.get().success(function (result) {
        $scope.profile = result;
    });

    db.company.search({}, 1, 5).success(function (result) {
        $scope.matching = result.items;
    });

}]);