﻿'    Sour Water Property Package 
'    Copyright 2016 Daniel Wagner O. de Medeiros
'
'    This file is part of DWSIM.
'
'    DWSIM is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    DWSIM is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with DWSIM.  If not, see <http://www.gnu.org/licenses/>.
'
'
'
'    Based on the SWEQ model described in the USEPA Report EPA-600/2-80-067: 
'    'A new correlation of NH3, CO2, and H2S volatility data from aqueous sour 
'    water systems', by Wilson, Grant M.  
'    Available online at http://nepis.epa.gov/Exe/ZyPDF.cgi?Dockey=9101B309.PDF

Imports DWSIM.DWSIM.SimulationObjects.PropertyPackages
Imports DWSIM.DWSIM.SimulationObjects.PropertyPackages.Auxiliary
Imports DWSIM.DWSIM.MathEx
Imports System.Linq
Imports DWSIM.DWSIM.ClassesBasicasTermodinamica

Namespace DWSIM.SimulationObjects.PropertyPackages

    <System.Runtime.InteropServices.Guid(SteamTablesPropertyPackage.ClassId)> _
<System.Serializable()> Public Class SourWaterPropertyPackage

        Inherits ActivityCoefficientPropertyPackage

        Public Shadows Const ClassId As String = "79aefcf3-6c17-4e44-91d4-b70b7642bb78"

        Public Sub New(ByVal comode As Boolean)

            MyBase.New(comode)

            Me.m_act = New Auxiliary.NRTL

        End Sub

        Public Sub New()

            MyBase.New(False)

            Me.m_act = New Auxiliary.NRTL

            Me.IsConfigurable = True
            Me.ConfigForm = New FormConfigNRTL
            Me._packagetype = PropertyPackages.PackageType.ActivityCoefficient

        End Sub

        Public Overrides Sub ReconfigureConfigForm()

            MyBase.ReconfigureConfigForm()
            Me.ConfigForm = New FormConfigNRTL

        End Sub

        Public Overrides ReadOnly Property FlashBase() As Auxiliary.FlashAlgorithms.FlashAlgorithm
            Get
                Dim constprops As New List(Of ConstantProperties)
                For Each su As Substancia In Me.CurrentMaterialStream.Fases(0).Componentes.Values
                    constprops.Add(su.ConstantProperties)
                Next
                Return New Auxiliary.FlashAlgorithms.SourWater With {.CompoundProperties = constprops}
            End Get
        End Property

    End Class

End Namespace