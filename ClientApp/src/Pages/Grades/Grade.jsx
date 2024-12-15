import axios from "axios";
import { Button } from "primereact/button"
import { Column } from "primereact/column"
import { ConfirmDialog } from "primereact/confirmdialog"
import { DataTable } from "primereact/datatable"
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { apiGrade, apiListGrade } from "../../utils/ApiConfig";
import { Bounce, toast } from "react-toastify";


function Grade(){
    const navigate = useNavigate();
    
    const [grades, setGrades] = useState(null);
    const [selectedGrades, setSelectedGrades] = useState(null);
    const [visible, setVisible] = useState(null);

    useEffect(() => {
        loadData()
    }, []);

    const loadData=()=>{
        axios.get(`${apiListGrade}`).then((response) => {
            setGrades(response.data.result);
            });
    }

    const handleDelete=(row)=>{
        axios.delete(`${apiGrade}/${row.idGrade}`).then((response) => {
            if(!response.data.hasError){
                loadData()
                alertSuccess('Data deleted')
            }else{
                console.log(response.data.mensaje)
            }
            
            });
    }
    const handleEdit=(row)=>{
        if(row){
            navigate(`/grade/${row.idGrade}`);
        }
    }
    const handleNew=()=>{
        navigate(`/grade/0`);
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

return(
    <section className="page-section" id="grade">
            <div className="container">
                <div className="text-center">
                    <h2 className="section-heading text-uppercase">Grades</h2>
                    <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                </div>
                <div className="row text-center">
                                <div className="col-md-4 text-left">
                                <Button onClick={() => handleNew()} label="New" severity="success" />   
                                </div>
                                
                                { grades ? 
                                <DataTable value={grades} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} selectionMode="single"  selection={selectedGrades} onSelectionChange={(e) => setSelectedGrades(e.value)} tableStyle={{ minWidth: '60rem' }}>
                                <Column field="name" header="Name"></Column>
                                <Column header="Teacher Name" body={(row) => (
                                    <>
                                    <label>{row.teacher.name} {row.teacher.surName}</label>
                                    </>
                                    )}>
                                </Column>
                                <Column header="" body={(row) => (
                                    <>
                                    <ConfirmDialog group="declarative"  visible={visible} onHide={() => setVisible(false)} message="Are you sure you want to delete?" 
                                        header="Confirmation" icon="pi pi-exclamation-triangle" accept={() => handleDelete(row) }  />
                                    <Button onClick={() => handleEdit(row)} icon="pi pi-check" label="Edit" severity="warning"/>
                                    <Button onClick={() => setVisible(true)} icon="pi pi-check" label="Delete" severity="danger"/>
                                    </>
                                    )}>
                                </Column>
                                </DataTable>
                                : <div></div>}
                                </div>
            </div>
        </section>
)
}
export default Grade