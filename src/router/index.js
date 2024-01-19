import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DashboardView from "@/views/dashboard/DashboardView.vue";
import DataView from "@/views/dashboard/DataView/DataView.vue";
import EmployeeList from "@/views/dashboard/DataView/employees/root/EmployeeList.vue";
import CompaniesList from "@/views/dashboard/DataView/companies/root/CompanyList.vue";
import CompanyCreate from "@/views/dashboard/DataView/companies/root/CompanyCreate.vue";
import CompanyUpdate from "@/views/dashboard/DataView/companies/root/CompanyUpdate.vue";
import EmployeeCreate from "@/views/dashboard/DataView/employees/root/EmployeeCreate.vue";
import EmployeeUpdate from "@/views/dashboard/DataView/employees/root/EmployeeUpdate.vue";
import VariantUpdate from "@/views/dashboard/DataView/variants/root/VariantUpdate.vue";
import VariantCreate from "@/views/dashboard/DataView/variants/root/VariantCreate.vue";
import VariantList from "@/views/dashboard/DataView/variants/root/VariantList.vue";
import DocumentList from "@/views/dashboard/DataView/documents/root/DocumentList.vue";
import DocumentCreate from "@/views/dashboard/DataView/documents/root/DocumentCreate.vue";
import DocumentUpdate from "@/views/dashboard/DataView/documents/root/DocumentUpdate.vue";
import StatusList from "@/views/dashboard/DataView/status/root/StatusList.vue";
import StatusCreate from "@/views/dashboard/DataView/status/root/StatusCreate.vue";
import StatusUpdate from "@/views/dashboard/DataView/status/root/StatusUpdate.vue";
import EmployeeQuickCreate from "@/views/dashboard/DataView/employees/root/EmployeeQuickCreate.vue";
import DocumentQuickCreate from "@/views/dashboard/DataView/documents/root/DocumentQuickCreate.vue";
import StatusQuickCreate from "@/views/dashboard/DataView/status/root/StatusQuickCreate.vue";
import VariantQuickCreate from "@/views/dashboard/DataView/variants/root/VariantQuickCreate.vue";
import CompanyQuickCreate from "@/views/dashboard/DataView/companies/root/CompanyQuickCreate.vue";
import ActionFunctionList from "@/views/dashboard/DataView/action/root/ActionFunctionList.vue";
import ActionFunctionQuickCreate from "@/views/dashboard/DataView/action/root/ActionFunctionQuickCreate.vue";
import ActionFunctionCreate from "@/views/dashboard/DataView/action/root/ActionFunctionCreate.vue";
import ActionFunctionUpdate from "@/views/dashboard/DataView/action/root/ActionFunctionUpdate.vue";
import DocumentTypeQuickCreate from "@/views/dashboard/DataView/documentTypes/root/DocumentTypeQuickCreate.vue";
import DocumentTypeList from "@/views/dashboard/DataView/documentTypes/root/DocumentTypeList.vue";
import DocumentTypeCreate from "@/views/dashboard/DataView/documentTypes/root/DocumentTypeCreate.vue";
import DocumentTypeUpdate from "@/views/dashboard/DataView/documentTypes/root/DocumentTypeUpdate.vue";
import MailList from "@/views/dashboard/DataView/mails/root/MailList.vue";
import MailQuickCreate from "@/views/dashboard/DataView/mails/root/MailQuickCreate.vue";
import MailCreate from "@/views/dashboard/DataView/mails/root/MailCreate.vue";
import MailUpdate from "@/views/dashboard/DataView/mails/root/MailUpdate.vue";
import HouseList from "@/views/dashboard/DataView/houses/root/HouseList.vue";
import HouseQuickCreate from "@/views/dashboard/DataView/houses/root/HouseQuickCreate.vue";
import HouseCreate from "@/views/dashboard/DataView/houses/root/HouseCreate.vue";
import HouseUpdate from "@/views/dashboard/DataView/houses/root/HouseUpdate.vue";
import CarUpdate from "@/views/dashboard/DataView/cars/root/CarUpdate.vue";
import CarList from "@/views/dashboard/DataView/cars/root/CarList.vue";
import CarQuickCreate from "@/views/dashboard/DataView/cars/root/CarQuickCreate.vue";
import CarCreate from "@/views/dashboard/DataView/cars/root/CarCreate.vue";
import OfferList from "@/views/dashboard/DataView/offers/root/OfferList.vue";
import OfferQuickCreate from "@/views/dashboard/DataView/offers/root/OfferQuickCreate.vue";
import OfferCreate from "@/views/dashboard/DataView/offers/root/OfferCreate.vue";
import OfferUpdate from "@/views/dashboard/DataView/offers/root/OfferUpdate.vue";
import RecruitmentView from "@/views/dashboard/Recruitment/RecruitmentView.vue";
import RecruitmentList from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/RecruitmentList.vue";
import RecruitmentCreate from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/RecruitmentCreate.vue";
import RecruitmentQuickCreate
  from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/RecruitmentQuickCreate.vue";
import RecruitmentUpdate from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/RecruitmentUpdate.vue";
import PlanningView from "@/views/dashboard/Planning/PlanningView.vue";
import PlanView from "@/views/dashboard/Planning/Planning/PlanView.vue";

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: DashboardView,
    children:[
      {
        path:'recruitment',
        name:'Recruitment',
        component: RecruitmentView,
        children:[
          {
            path: 'processes',
            name: 'RecruitmentList',
            component: RecruitmentList,
            children:[
              {
                path:'quick',
                name:'RecruitmentQuickCreate',
                component:RecruitmentQuickCreate
              }
            ]
          },
          {
            path:'processes/create',
            name:'RecruitmentCreate',
            component:RecruitmentCreate,
          },
          {
            path:'processes/manage/:id',
            name:'RecruitmentUpdate',
            component:RecruitmentUpdate,
          },
        ]
      },
      {
        path:'planning',
        name:'Planning',
        component: PlanningView,
        children:[
          {
            path: 'plan',
            name: 'Plan',
            component: PlanView,
          },
        ]
      },
      {
        path:'management',
        name:'Management',
        component: DataView,
        children:[
          {
            path:'employees',
            name:'EmployeeList',
            component:EmployeeList,
            children:[
              {
                path:'quick',
                name:'EmployeeQuickCreate',
                component:EmployeeQuickCreate
              }
            ]
          },
          {
            path:'employees/create',
            name:'EmployeeCreate',
            component:EmployeeCreate,
          },
          {
            path:'employees/update/:id',
            name:'EmployeeUpdate',
            component:EmployeeUpdate,
          },
          {
            path:'companies',
            name:'CompanyList',
            component:CompaniesList,
            children:[
              {
                path:'quick',
                name:'CompanyQuickCreate',
                component:CompanyQuickCreate
              }
            ]
          },
          {
            path:'companies/create',
            name:'CompanyCreate',
            component:CompanyCreate,
          },
          {
            path:'companies/update/:id',
            name:'CompanyUpdate',
            component:CompanyUpdate,
          }
          ,
          {
            path:'variants',
            name:'VariantList',
            component:VariantList,
            children:[
              {
                path:'quick',
                name:'VariantQuickCreate',
                component:VariantQuickCreate
              }
            ]
          },
          {
            path:'variants/create',
            name:'VariantCreate',
            component:VariantCreate,
          },
          {
            path:'variants/update/:id',
            name:'VariantUpdate',
            component:VariantUpdate,
          }
          ,
          {
            path:'documents',
            name:'DocumentList',
            component:DocumentList,
            children:[
              {
                path:'quick',
                name:'DocumentQuickCreate',
                component:DocumentQuickCreate
              }
            ]
          },
          {
            path:'documents/create',
            name:'DocumentCreate',
            component:DocumentCreate,
          },
          {
            path:'documents/update/:id',
            name:'DocumentUpdate',
            component:DocumentUpdate,
          },
            //
          {
            path:'document-types',
            name:'DocumentTypeList',
            component:DocumentTypeList,
            children:[
              {
                path:'quick',
                name:'DocumentTypeQuickCreate',
                component:DocumentTypeQuickCreate
              }
            ]
          },
          {
            path:'document-types/create',
            name:'DocumentTypeCreate',
            component:DocumentTypeCreate,
          },
          {
            path:'document-types/update/:id',
            name:'DocumentTypeUpdate',
            component:DocumentTypeUpdate,
          },
            //
          {
            path:'mails',
            name:'MailList',
            component:MailList,
            children:[
              {
                path:'quick',
                name:'MailQuickCreate',
                component:MailQuickCreate
              }
            ]
          },
          {
            path:'mails/create',
            name:'MailCreate',
            component:MailCreate,
          },
          {
            path:'mails/update/:id',
            name:'MailUpdate',
            component:MailUpdate,
          },
          {
            path:'status',
            name:'StatusList',
            component:StatusList,
            children:[
              {
                path:'quick',
                name:'StatusQuickCreate',
                component:StatusQuickCreate
              }
            ]
          },
          {
            path:'status/create',
            name:'StatusCreate',
            component:StatusCreate,
          },
          {
            path:'status/update/:id',
            name:'StatusUpdate',
            component:StatusUpdate,
          },
          {
            path:'action-function',
            name:'ActionFunctionList',
            component:ActionFunctionList,
            children:[
              {
                path:'quick',
                name:'ActionFunctionQuickCreate',
                component:ActionFunctionQuickCreate
              }
            ]
          },
          {
            path:'action-function/create',
            name:'ActionFunctionCreate',
            component:ActionFunctionCreate,
          },
          {
            path:'action-function/update/:id',
            name:'ActionFunctionUpdate',
            component:ActionFunctionUpdate,
          },
          {
            path:'house',
            name:'HouseList',
            component:HouseList,
            children:[
              {
                path:'quick',
                name:'HouseQuickCreate',
                component:HouseQuickCreate
              }
            ]
          },
          {
            path:'house/create',
            name:'HouseCreate',
            component:HouseCreate,
          },
          {
            path:'house/update/:id',
            name:'HouseUpdate',
            component:HouseUpdate,
          },
          {
            path:'cars',
            name:'CarList',
            component:CarList,
            children:[
              {
                path:'quick',
                name:'CarQuickCreate',
                component:CarQuickCreate
              }
            ]
          },
          {
            path:'cars/create',
            name:'CarCreate',
            component:CarCreate,
          },
          {
            path:'cars/update/:id',
            name:'CarUpdate',
            component:CarUpdate,
          },
          {
            path:'offers',
            name:'OfferList',
            component:OfferList,
            children:[
              {
                path:'quick',
                name:'OfferQuickCreate',
                component:OfferQuickCreate
              }
            ]
          },
          {
            path:'offers/create',
            name:'OfferCreate',
            component:OfferCreate,
          },
          {
            path:'offers/update/:id',
            name:'OfferUpdate',
            component:OfferUpdate,
          },
        ]
      }
    ]
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})




export default router
