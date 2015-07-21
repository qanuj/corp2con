app.controller('contractorFavoriteController', ['$scope', 'dataService', function ($scope,db) {
    $scope.title = "Contractor : Search Result";
    $scope.query= {
        keywords: '',
        location: '',
        skills:'',
    }
 
}]);