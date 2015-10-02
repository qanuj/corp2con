app.controller('publicSearchController', ['$scope', 'dataService', '$rootScope', '$state', '$stateParams', function ($scope, db, $rootScope, $state, $stateParams) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
    $scope.hideCheckbox = true;
    $scope.title = "Jobs : Search Result";
    var param = db.args($stateParams);
    
    $scope.query = {
        keywords: param.q || param.keywords || '',
        location: param.location || '',
        skills: param.skills || '',
        startrate: param.startrate || '',
        endrate: param.endrate || '',
        xfrom: param.xfrom || '',
        xto: param.xto || '',
        salaryFrom: param.salaryfrom || '',
        salaryTo: param.salaryto || ''
    }

    function fetchResults(query, page) {
        db.job.search(query, page).then(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
            $scope.page = page;
            $scope.selectAll = false;
        });
    }


    $scope.search = function (query) {
        var q = '';
        for (var x in query) {
            q += (q === '' ? '?' : '&') + x + '=' + query[x];
        }
        fetchResults(query, 1);
    }

    $scope.navigate = function (page) {
        fetchResults($scope.query, page || 1);
    }

    $scope.toggle = function (allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });
    
    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });

    db.system.getFunctionals().success(function (result) {
        $scope.functionals = result;
    });

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        return value + 'y';
    }

    $scope.rateTranslate = function (value) {
        return value + 'k';
    }


    //Slider configs
    $scope.experienceSlider = {
        min: $scope.query.xfrom || 0,
        max: $scope.query.xto || 25,
        ceil: 25,
        floor: 0
    };

    $scope.salarySlider = {
        min: $scope.query.salaryfrom || 0,
        max: $scope.query.salaryto || 30,
        ceil: 30,
        floor: 0
    };


    $scope.translate = function (value) {
        return '₹' + value+'lacs';
    }

    $scope.move = function (folder) {
        var i = 0;
        function moveFolder(x, folder, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.company.moveContractor($scope.records[x].id, folder).success(next);
            } else next();
        }
        function onNext() {
            moveFolder(++i, folder, onNext);
        }
        moveFolder(i, folder, onNext);
    }


    $scope.resetFilters = function () {
        $scope.query.keywords = '';
        $scope.query.skills = '';
        $scope.query.location = '';
        $scope.query.startrate = '';
        $scope.query.endrate = '';
        $scope.query.xfrom = '';
        $scope.query.xto = '';
        $scope.query.industry = '';
        $scope.search(query);
    }

    $scope.$watch('selectAll', function (val) {
        $scope.toggle($scope.selectAll);
        console.log('Toggeling', val);
    });


    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        $scope.query.salaryfrom = $scope.salarySlider.min;
        $scope.query.salaryto = $scope.salarySlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);
    });

    $scope.navigate(param.page);

}]);