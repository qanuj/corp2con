app.controller('adminCountryController', ['$scope', 'dataService', '$routeParams','$rootScope', function ($scope, db, params,$rootScope) {
$scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Country";

    $scope.navigate = function (page) {
        db.system.pagedCountry(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
        

    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
            db.system.addFunctional(record).success($scope.navigate());
        }

        function refreshRecord() {
            return db.system.getFunctionals().success(function (result) {
                $scope.records = result.items;
            });
    }

    $scope.update = function (record) {
            db.system.updateFunctional(record).success($scope.navigate(params.page));
    };

    $scope.delete = function (record) {
            db.system.deleteFunctional(record).success($scope.navigate(params.page));
    };

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    };
    }

    $scope.navigate(params.page);


}]);

