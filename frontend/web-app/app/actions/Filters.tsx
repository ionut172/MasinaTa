
import { useParamsStore } from "@/hooks/useParamsStore";
import { Button } from "flowbite-react";
import ButtonGroup from "flowbite-react/lib/esm/components/Button/ButtonGroup";
import React from "react";
import { IconBase } from "react-icons";
import { AiOutlineClockCircle, AiOutlineSortAscending } from "react-icons/ai";
import { BsFillStopCircleFill, BsStopwatchFill } from "react-icons/bs"
import {GiFinishLine, GiFlame} from "react-icons/gi";
type Props =  {
    pageSize: number;
    setPageSize: (size: number) => void;

}
const pageSizeButtons =[4,6,12];
const orderButtons = [
    {
        label: "Alfabetic",
        icon: AiOutlineSortAscending,
        value: "make"
    },
    {
        label: "Timp finalizare",
        icon: AiOutlineClockCircle,
        value: "endingSoon"
    },
    {
        label: "Recent adaugate",
        icon: BsFillStopCircleFill,
        value: "new"
    }
]
const filterButtons = [
    {
        label: "Licitatii Live",
        icon: GiFlame,
        value: "live"
    },
    {
        label: "<6 ore",
        icon: GiFinishLine,
        value: "endingSoon"
    },
    {
        label: "Terminate",
        icon: BsStopwatchFill,
        value: "finished"
    }
]

export default function Filters(){
    const pageSize = useParamsStore(state => state.pageSize);
    const setParams = useParamsStore(state => state.setParams);
    const orderBy = useParamsStore(state=>state.orderBy);
    const filterBy = useParamsStore(state=>state.filterBy);
    return (
        <div className="flex justify-between items-center mb-4">
            
            <div>
            <span className="upercase text-sm text-gray-500 mr-2">Filter By</span>
            <ButtonGroup>
                {filterButtons.map(({label, icon:IconBase, value})=> (
                    <Button key={value} onClick={()=>setParams({filterBy: value})} color={`${filterBy === value ? "red" : "gray"}`}>
                       
                        {label}
                    </Button>
                ))}
            </ButtonGroup>
            </div>

            <div>
            <span className="upercase text-sm text-gray-500 mr-2">Order By</span>
            <ButtonGroup>
                {orderButtons.map(({label, icon:IconBase, value})=> (
                    <Button key={value} onClick={()=>setParams({orderBy: value})} color={`${orderBy === value ? "red" : "gray"}`}>
                       
                        {label}
                    </Button>
                ))}
            </ButtonGroup>
            </div>
            <div>
                <span className="upercase text-sm text-gray-500 mr-2">Page size</span>
                <ButtonGroup>
                    {pageSizeButtons.map((value, i)=> (
                        <Button key={i} onClick={()=> setParams({pageSize:value})}
                        color={`${pageSize === value ? 'red' : 'gray'}`}
                        className="focus:ring-0"
                        >
                        {value}
                        </Button>
                    ))}
                </ButtonGroup>
                </div>
        </div>
    )
}