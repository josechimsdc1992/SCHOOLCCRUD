import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { apiTeacher } from "../../utils/ApiConfig";
import { Bounce, toast, ToastContainer } from "react-toastify";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Badge } from "primereact/badge";
import { Dropdown } from "primereact/dropdown";

const getEditarForm = () => ({
    idStudent: { value: 0, error: false },
    name: { value: '', error: false },
    surName: { value: '', error: false },
    genero: { value: '', error: false },
    date: { value: null, error: false }
});

function TeacherCU(){
    const navigate = useNavigate();
    let param = useParams();

    const [form, setForm] = useState(getEditarForm());
    const [hasError,setHasError]=useState(false)

    const genders= [
        {label: 'Female', value: 'F'},
        {label: 'Male', value: 'M'},
    ];
    
    useEffect(() => {
        if(param.id!=0){
            axios.get(`${apiTeacher}/${param.id}`).then((response) => {
                if(!response.data.hasError){
                    loadForm(response.data.result)
                }else{
                    console.log(response.data.mensaje)
                }
                
                });
        }
        
        
    }, []);

    const handleBack=()=>{
        navigate(-1)
    }

    const handleSave=()=>{

        handleValidate()

        if(hasError)
            return;

        const teacher=
        {
            "idTeacher": form.idTeacher.value,
            "name": form.name.value,
            "surName": form.surName.value,
            "genero": form.genero.value
          }

        
        axios
        .post(`${apiTeacher}`, teacher)
        .then((response) => {
            if(!response.data.hasError){
                console.log(response.data)
                loadForm(response.data.result)
                alertSuccess('Data saved')
            }else{
                console.log(response.data.mensaje)
            }
            
        });
        
    }

    const handleValidate=()=>{
        setHasError(false)

        if(form.name.value==""){
            setHasError(true)
            form.name.error=true;
        }
        if(form.surName.value==""){
            setHasError(true)
            form.surName.error=true;
        }
        if(form.genero.value==""){
            setHasError(true)
            form.genero.error=true;
        }
    }

    const alertSuccess= async (text) => {
        toast.success(text, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
            }
            );
        
    };

    const handleChangeForm = async (e) => {
        let error=false
        if(e.target.value==""){
            error=true
        }
        setForm({
            ...form,
            [e.target.name]: {
              value: e.target.value,
              error: error,
            },
          });
        
    };

    const loadForm=(result)=>{
        setForm({
            idTeacher: { value: result.idTeacher, error: false },
            name: { value: result.name, error: false },
            surName: { value: result.surName, error: false },
            genero: { value: result.genero, error: false }
        })
    }
    return (
            <section className="page-section" id="services">
                        <div className="container">
                            <div className="text-center">
                                <h2 className="section-heading text-uppercase">{param.id==0?'Add Student':'Edit Student'}</h2>
                                <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                            </div>
                            <div className="row text-center">
                            <div className="col-md-4 text-left">
                            <Button onClick={() => handleBack()} label="Back" severity="info" />
                                    
                            </div>
                            <div className="flex flex-column gap-2">
                                <label htmlFor="name">Name</label>
                                <InputText id="name" name="name"  value={form.name.value}  onChange={(e) => handleChangeForm(e)}/>
                                { form.name.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                                
                            </div>
                            <div className="flex flex-column gap-2">
                                <label htmlFor="surname">Sur Name</label>
                                <InputText id="surname" name="surName" value={form.surName.value}  onChange={(e) => handleChangeForm(e)}/>
                                { form.surName.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                            </div>
                            <div className="flex flex-column gap-2">
                                <label htmlFor="gender">Gender</label>
                                <Dropdown optionLabel="label" name="genero" value={form.genero.value} options={genders} onChange={(e) => handleChangeForm(e)} placeholder="Select a Gender"/>
                                { form.genero.error ? <Badge value="Required" severity="danger"></Badge> :<></> }
                            </div>
                           
                            <div className="col-md-4 text-left">
                            <Button onClick={() => handleSave()} label="Save" severity="success" />
                                    
                            </div>
    
                            </div>
                        </div>
                        <ToastContainer />
                </section>
          )

}
export default TeacherCU