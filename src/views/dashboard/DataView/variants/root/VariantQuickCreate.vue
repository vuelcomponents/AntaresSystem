

<template>
<div class="multiple-create" :key="life.counterKey">
  <div :hidden="hide(param)" class="box" v-for="param in variant.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="variant[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in variant.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="variant.getHeaders([param],'first')" class="form-text-input" v-model="variant[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in variant.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="variant[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in variant.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="variant[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   variant.create.includes(param) ? 'create' : '',
                                   variant.select.includes(param) ? 'select' : '',
                                   variant.delete.includes(param) ? 'delete' : '',
                                   variant.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="variant"
                         owner-key="variant"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="variant[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in variant.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="variant" :param="param" :disabled="lock(param)" :model="variant"></SelectionManager>
    </div>
  </div>
  <div v-for="param in variant.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="variant.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="variant.setFile($event, param)" />
    </div>
  </div>
  <div class="box" v-for="param in variant.getInputs('booleans')"  :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Checks </span>
    </p>
    <div style="display:flex; justify-content:flex-start; width:100%">
      <InputSwitch :disabled="lock(param)"  v-model="variant[param]" ></InputSwitch>
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>




</template>
<script>

import { inject } from "vue";
import VariantCrudController from "@/views/dashboard/DataView/variants/controllers/VariantCrudController";
import Toast from "@/utils/classes/Toast";
import VariantClass from "@/views/dashboard/DataView/variants/VariantClass";
import VariantCreateOptions from "@/views/dashboard/DataView/variants/options/VariantCreateOptions";
import VariantLife from "@/views/dashboard/DataView/variants/VariantLife";
import {VariantService} from "@/services/VariantService";
export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new VariantService(emitter)
    };
    const toast = new Toast()
    const crud = new VariantCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      variant: new VariantClass(),
      popupOptions: new VariantCreateOptions(this.$router, this.create),
      life: new VariantLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.variant).then(res=>{
        if(res?.invalid){
          console.log('invalid variant create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) {
            this.emitter.emit('quickVariantAdd', res.data)
          }
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.variant[key] = collection
      }
    },
    lock(param){
      if(!this.variant.$lock[param]){
        return false;
      }
      return this.variant.$lock[param]()
    },
    hide(param){
      if(!this.variant.$hide[param]){
        return false;
      }
      return this.variant.$hide[param]()
    }
  },
  watch: {
    "variant.storeModel"() {
      this.life.counterKey++;
    },
  }
}
</script>
<style >

</style>