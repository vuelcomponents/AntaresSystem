<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in variant.getInputs('text')" :key="param">
          <p class="form-label">
            {{variant.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="variant[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in variant.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{variant.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="variant.getHeaders([param],'first')" class="form-text-input" v-model="variant[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in variant.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{variant.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="variant[param]" :key="variant[param]" :setter="(value)=>variant.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in variant.getInputs('collections')" :hidden="hide(param)" :key="param">
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
                             :owner="variant"
                             owner-key="variant"
                             :emitter="emitter"
                             :set-collection="setCollection"
                             :collection-key="param"
                             :collection="variant[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in variant.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{variant.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="variant" :param="param" :disabled="lock(param)" :model="variant"></SelectionManager>
          </div>
        </div>
        <div v-for="param in variant.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
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
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          left
        </div>
      </template>
      <template v-slot:item2contentRight>
        <div class="box">
          right
        </div>
      </template>
      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>
      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>
import DataPopup from "@/components/popups/DataPopup.vue";
import { inject } from "vue";
import Toast from "@/utils/classes/Toast";
import VariantEditOptions from "@/views/dashboard/DataView/variants/options/VariantEditOptions";
import VariantCrudController from "@/views/dashboard/DataView/variants/controllers/VariantCrudController";
import VariantClass from "@/views/dashboard/DataView/variants/VariantClass";
import {VariantService} from "@/services/VariantService";

import VariantLife from "@/views/dashboard/DataView/variants/VariantLife";
import {VariantRealisationService} from "@/services/VariantRealisationService";
import VariantRealisationClass from "@/views/dashboard/DataView/variants/VariantRealisationClass";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import SelectionManager from "@/components/SelectionManager/SelectionManager.vue";


export default {
  computed: {
    VariantRealisationClass() {
      return VariantRealisationClass
    }
  },
  components: {CollectionManager, DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      variant: new VariantService(emitter),
      variantRealisations: new VariantRealisationService(emitter),
    };
    const toast =new Toast();
    const crud = new VariantCrudController(services.variant, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      life:new VariantLife(),
      variant: new VariantClass(),
      popupOptions: new VariantEditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.variant = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.variant).then(res=>{
        if(res?.invalid){
          console.log('invalid variant create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
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
  }
}
</script>



<style>

</style>