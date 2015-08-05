app.controller('contractorApplicationController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {
    $scope.role = db.role;
    db.contractor.get(param.id).success(function (result) {
        return $scope.record = result;
    });

    db.job.get(param.jobID).success(function (result) {
        $scope.job = result;
        console.log($scope.job);
    });

    function jobId () {

    }

    $scope.accept = function () {

    }
}]);