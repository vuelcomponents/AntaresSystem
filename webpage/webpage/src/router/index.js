import { createRouter, createWebHistory } from 'vue-router'

import MainScreen from "@/screens/MainScreen.vue";
import WelcomeScreen from "@/screens/welcome/WelcomeScreen.vue";
import RecruitmentScreen from "@/screens/recruitment/RecruitmentScreen.vue";

const routes = [
  {
    path: '/',
    name: 'home',
    component: MainScreen,
    children:[
      {
        path:'',
        name:'WelcomeScreen',
        component:RecruitmentScreen
      }
    ]
  },
  {
    path: '/about',
    name: 'about',
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
