app.controller('myJobsController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "My Jobs";
    db.company.myJobs().success(function (result) {
        $scope.records = result;
        $scope.page = db.currentPage;
        console.log($scope.records);
    });
}]);