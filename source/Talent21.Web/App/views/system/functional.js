app.controller('functionalController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {
   
    $scope.navigate = function (page) {
        refreshRecord();
        db.system.pagedFunctional(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            console.log(result);
        });
        

        $scope.save = function (record) {
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            db.system.addFunctional(record).success(refreshRecord);
        }

        $scope.update = function (record) {
            db.system.updateFunctional(record).success(refreshRecord);
        };

        $scope.delete = function (record) {
            db.system.deleteFunctional(record).success(refreshRecord);
        };

        $scope.toggle = function (record) {
            record.editMode = !record.editMode;
        };
       

        function refreshRecord() {
            return db.system.getFunctionals().success(function (result) {
                $scope.records = result.items;
            });
        }
    }
   
    $scope.navigate(params.page);
    
   
}]);

