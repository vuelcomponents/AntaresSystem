<template>
  <header class="main-screen-header-container">
    <header class="main-screen-header">
        <img src="@/assets/logo.png"/>
        <p :class="{bold:1, 'r-link':1,  'r-link-active':routeName==='wts'}">WTS Uitzendbureau</p>
        <p :class="{'r-link':1, 'r-link-active':routeName==='WelcomeScreen'}">Recruitment</p>
        <p :class="{'r-link':1,  'r-link-active':routeName==='contrahents'}">Our contrahents</p>
        <p :class="{'r-link':1,  'r-link-active':routeName==='about'}">About WTS</p>
        <p :class="{'r-link':1,  'r-link-active':routeName==='contact'}">Contact</p>
    </header>
    <header class="main-screen-header-right">
      <p class="r-link" @click="openLoginScreen">Inloggen</p>
      <p class="r-link" @click="openRegisterScreen">Register</p>
    </header>
  </header>
  <main class="bar-header-popups">
    <RegisterScreen ref="registerScreen"/>
    <LoginScreen ref="loginScreen"/>
  </main>
</template>
<script>
import RegisterScreen from "@/screens/user/RegisterScreen.vue";
import LoginScreen from "@/screens/user/LoginScreen.vue";

export default {
  name: "BarHeader",
  components:{RegisterScreen, LoginScreen},
  methods:{
    openRegisterScreen(){
      this.$refs.registerScreen.screenOn = true;
    },
    openLoginScreen(){
      this.$refs.loginScreen.screenOn = true;
    },
  },
  data(){
    return{
      routeName:this.$route.name
    }
  },
  mounted(){

  },
}
</script>
<style scoped lang="scss">

</style>