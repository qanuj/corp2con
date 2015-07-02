app.controller('jobProfileController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Job Profile";
    db.job.profile().success(function (result) {
        $scope.record = result;
        console.log($scope.record);
    });
}]);