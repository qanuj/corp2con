app.factory('dataService', ['$http', function ($http) {
    var v = 'api/v1/';
    var factory = {
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    function calculatePaging(page, pageSize) {
        pageSize = pageSize || 50;
        var pg = "&$top=" + pageSize;
        if (page > 1) {
            pg+="&$skip=" + ((page-1) * pageSize);
        }
        return pg;
    }

    factory.role = document.querySelector('html').dataset.role;

    factory.currentPage = window.location.href;

    //For Contractors

    factory.contractor.paged = function () {
        return $http.get(v + 'contractor/paged');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    factory.contractor.profile = function () {
        return $http.get(v + 'contractor/profile');
    }

    factory.contractor.editProfile = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    factory.contractor.searchJob = function (query) {
        return $http.post(v + 'job/search', query);
    }

    factory.contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    factory.contractor.ApplyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    factory.contractor.favorite = function (id) {
        return $http.put(v + 'candidate/job/application/' + id + '/favorite');
    }

    factory.userid = "" ;


    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    factory.contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    factory.contractor.deleteSchedule = function (formData) {
        return $http.delete(v + 'contractor/schedule', formData);
    }

    factory.contractor.getSchedule = function (page) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page));
    }


    //For companies

    factory.company.paged = function () {
        return $http.get(v + 'company/paged?$inlinecount=allpages');
    }

    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.profile = function () {
        return $http.get(v + 'company/profile');
    }

    factory.company.newjob = function (formData) {
        return $http.put(v + 'company/job', formData);
    }

    factory.company.editProfile = function (formData) {
        return $http.put(v + 'company/profile', formData);
    }

    factory.company.myJobs = function () {
        return $http.get(v + 'company/job/all');
    }

    factory.company.limitJobs = function () {
        return $http.get(v + 'company/job/paged');
    }

    factory.job.publish = function (record) {
        return $http.put( v + 'company/job/publish', record);
    }

    factory.job.cancel = function (record) {
        return $http.put(v + 'company/job/cancel', record);
    }

    factory.job.delete = function (record) {
        return $http.delete(v + 'company/job', record);
    }
    
    factory.company.search = function (query) {
        return $http.post(v + 'company/search', query);
    }   
    //For Jobs

    factory.job.profile = function (id) {
        return $http.get(v + 'company/job/ ' + id);
    }

    factory.job.newjob = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    factory.job.editjob = function (record) {
        return $http.put(v + 'company/job', record);
    }

    //System requests

    factory.system.searchLocations = function (address) {
        return $http.get('//maps.googleapis.com/maps/api/geocode/json', { params: { address: address, sensor: false } });
    }

    factory.system.getSkills = function () {
        return $http.get(v + 'system/skill/all');
    }

    factory.system.getIndustries = function () {
        return $http.get(v + 'system/industry/all');
    }

    return factory;
}]);