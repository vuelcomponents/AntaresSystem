<script>
import {Employee} from "@/screens/user/Employee";
import { useForm, useField } from 'vee-validate';
export default {
  name: "RegisterScreen",

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
        <h2 class="pretty-h mdi mdi-account-card-outline"></h2>
        <form @submit.prevent="submit">
          <label for="firstName">First Name</label>
          <input-text v-model="employee.firstName" class="blue-input" name="firstName" type="text" />
          <label for="lastName">Last Name</label>
          <input-text v-model="employee.lastName" class="blue-input" name="lastName" type="text" />
          <label for="email">Email</label>
          <input-text v-model="employee.email" class="blue-input" name="email" type="text" />
          <label for="password">Password</label>
          <input-text v-model="employee.password" class="blue-input" name="password" type="password" />
          <label for="repassword">Reenter Password</label>
          <input-text v-model="employee.repassword" class="blue-input" name="repassword" type="password" />
          <Button style="width:200px" type="submit" icon="mdi mdi-account-plus-outline" label="Register"/>
        </form>
      </section>
    </div>
  </section>
</section>
</template>

<style scoped lang="scss">

</style>