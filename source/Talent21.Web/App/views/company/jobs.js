app.controller('jobsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {
    $scope.title = "Jobs";
    $scope.navigate=function(page) {
        db.company.myJobs(page).success(function (result) {
            $scope.currentPage = page||1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            angular.forEach(result.items, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.records = result.items;
        });
    }
    $scope.navigate(params.page);
}]);