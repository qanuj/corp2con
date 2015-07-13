app.controller('jobsController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Jobs";
    db.company.myJobs().success(function (result) {
        $scope.records = result;
        $scope.page = db.currentPage;
        console.log($scope.records);
    });
}]);