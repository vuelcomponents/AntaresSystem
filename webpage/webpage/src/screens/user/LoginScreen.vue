<script>
import {Employee} from "@/screens/user/Employee";
export default {
  name: "LoginScreen",

  data(){
    return{
      screenOn:false,
      employee:new Employee()
    }
  },
  methods: {
    submit(){
      console.log('submit')
    }
  }
}
</script>

<template>
  <section class="bg-black" v-if="screenOn" @click.self="screenOn=false">
    <section class="register-screen">
      <div class="pretty-screen">
        <section class="register-screen-node">
          <h2 class="pretty-h mdi mdi-key"></h2>
          <form @submit.prevent="submit">
            <label for="email">Email</label>
            <input-text v-model="employee.email" class="blue-input" name="email" type="text" />
            <label for="password">Password</label>
            <input-text v-model="employee.password" class="blue-input" name="password" type="password" />
            <Button style="width:200px" type="submit" icon="mdi mdi-door" label="Login"/>
          </form>
        </section>
      </div>
    </section>
  </section>
</template>

<style scoped lang="scss">

</style>