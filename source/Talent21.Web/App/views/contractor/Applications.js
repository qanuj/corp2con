app.controller('contractorApplicationsController', ['$scope', 'dataService', '$stateParams', function ($scope, db, $stateParams) {
    $scope.title = "Job Applications";
    
    $scope.navigate = function (page) {
        var id = $stateParams.id;
        db.contractor.getJobApplications(id,page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.records = result.items;
               });              
    }
    $scope.navigate($stateParams.page);
}]);

