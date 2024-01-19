<template>
    <nav ref="navigation" id="nav"  :style="{ height:'100vh',marginTop: marginTop + 'px',  transition:'none' }">
      <section style="text-align:left; margin-left:30px;">
        <img @click="$router.push({name:'home'})" src="@/assets/refactor/logo.png" style="width:40%;cursor:pointer"/>
      </section>
        <section  :style="`height:60px;`" class="transition" v-for="item in this.menu" :key="item" >
            <div  @click="()=>{if(item.routeName){this.$router.push({name:item.routeName})}}" class="div-link-icon-div">
              <div class="link-icon-div">
                <i :class="'link-icon '+  item.icon ?? 'mdi mdi-link'"></i>
              </div>
                {{item.label}}
            </div>
            <div style="display:none;" class="hidden"></div>
        </section>
    </nav>
</template>

<script>

export default {
    name: "SideMenu",
    methods:{
      fitMenuSize(){
        setTimeout(()=>{
          if(this.$refs.navigation) {
            this.$refs.navigation.style.minWidth = this.$refs.navigation.parentElement.clientWidth + 'px'
            this.$refs.navigation.style.width = this.$refs.navigation.parentElement.clientWidth + 'px'
          }
        },100)

      }
    },
    data(){
        return {
            marginTop: 0,
            marginBottom: 0,
            pageSize:0,
            services:{

            },
            items:{
              Recruitment:[
                {
                  label:'Recruitment Process',
                  routeName:'RecruitmentList',
                  icon:'mdi mdi-account-multiple',
                  dropped:true,
                },
              ],
                Management:[
                    {
                        label:'Employees',
                        icon:'mdi mdi-account-multiple',
                        routeName:'EmployeeList',
                        dropped:false,
                    },
                    {
                        label:'Companies',
                        icon:'mdi mdi-domain',
                        dropped:false,
                        routeName:'CompanyList',
                    },
                  {
                    label: 'Variants',
                    routeName: 'VariantList',
                    icon: 'mdi mdi-shape',
                  },
                  {
                    label: 'Documents',
                    routeName: 'DocumentList',
                    icon: 'mdi mdi-file-document',
                  },
                  {
                    label: 'Document types',
                    routeName: 'DocumentTypeList',
                    icon: 'mdi mdi-file-document-outline',
                  },
                  {
                    label: 'Status',
                    routeName: 'StatusList',
                    icon: 'mdi mdi-check-circle',
                  },
                  {
                    label: 'Actions',
                    routeName: 'ActionFunctionList',
                    icon: 'mdi mdi-gesture-tap',
                  },
                  {
                    label: 'Mails',
                    routeName: 'MailList',
                    icon: 'mdi mdi-email-outline',
                  },
                  {
                    label: 'Offers',
                    routeName: 'OfferList',
                    icon: 'mdi mdi-gift-outline',
                  },
                  {
                    label: 'Houses',
                    routeName: 'HouseList',
                    icon: 'mdi mdi-home-outline',
                  },
                  {
                    label: 'Cars',
                    routeName: 'CarList',
                    icon: 'mdi mdi-car-outline',
                  },
                ],
                BonusSystem:[

                ],

            },
            module:null,
            menu:[],
        }
    },
    mounted(){
        this.menu = this.items[(this.$route.matched[1].name)];
        this.$nextTick(()=>{
          this.fitMenuSize()
        })
      window.addEventListener('resize', this.fitMenuSize)
      window.addEventListener('zoom', this.fitMenuSize)
    },


}
</script>

<style >
.hidden{
    display:none!important;
}


.height{
    height:40px;
}
.no-height{
    height:0;
}

 .sticky-navigation {
   position: sticky;
   top: 0;
   z-index: 9; /* Adjust the z-index as needed */
 }

</style>