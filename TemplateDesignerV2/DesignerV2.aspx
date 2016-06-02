﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignerV2.aspx.cs" Inherits="TemplateDesignerV2.DesignerV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head>
    <meta charset="utf-8" />
    <title>Template Designer</title>
    <meta charset="utf-8" />
    <link href="styles/DSv2.css" rel="stylesheet" />
    <link href="styles/sunny/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="js/a12/a66.css" rel="stylesheet" />
    <link href="styles/p103.css" rel="stylesheet" />
    <script src="js/p55.js"></script>
    <script src="js/p55-ease.js"></script>
    <script src="js/p40.js"></script>
    <script src="js/pvcc1.js"></script>
    <script src="js/pcf01-v2.js"></script>
    <script src="js/p64test.js"></script>
    <script src="js/p21-v2.js"></script>
    <script src="js/p15-v2.js"></script>

    <link href="styles/jquery.cropbox.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet" type="text/css" />
</head>
<body>

    <section id="content" class="preparing ">
        <section id="objectPanel" class="locked stage2" data-num-cols="4">

            <span id="selectedTab"><span></span><span></span></span>
            <menu class="mainLeftMenu">
                <li class="search on" style="display: none;"><a>Images</a></li>
                <li id="Template" class="layout"><a>Templates</a></li>
                <li id="Quick" class="QuickTxt"><a>Quick</a></li>
                <li id="btnAdd" class="text btnAdd"><a>Add</a></li>
                <li id="backgrounds" class="backgrounds"><a>Bkgrounds</a></li>
                <li id="uploads" class="uploads" style="display: none;"><a>Uploads</a></li>
                <li id="layersPanel" class="layersPanel"><a>Layers</a></li>
                <li id="layoutsPanel" class="layoutsPanel" style="display: none;"><a>Layouts</a></li>
            </menu>

            <section class="content mainContainer">
                <section id="resultsSearch" class="results infinite">
                    <%--	<section class="layer scrollPaneImgDam">
							<div class="inner">
								<div class="browseCategories ImgsBrowserCategories">
									<ul class="gallery photoGallery albumGallery categories laidOut" style="height: 129px;">
										<li style="width: 109px; left: 0px; top: 0px;" class="ImgLr1c1"><a title="Free Images" class="btnFreeImgs"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/travel.jpg" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
											<span class="text">Free Images</span> </a></li>
										<li style="width: 109px; left: 109px; top: 0px;" class="ImgLr1c2"><a title="Illustrations" class="btnIllustrations"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/handdrawn.jpg" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
											<span class="text">Illustrations</span> </a></li>
										<li style="width: 109px; left: 218px; top: 0px;" class="ImgLr1c3"><a title="Frames" class="btnFrames"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/frames.png" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
											<span class="text">Frames</span> </a></li>
									</ul>
									<section class="ImagesScrollContainer">
										<section class="freeImgsContainer ImgPanels disappearing Imr1c1">
											<div class="inlineFolderPointer" style="left: 84.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divGlobImgContainer" id="divGlobImgContainer"></ul>
											</div>
										</section>
										<section class="illustrationsContainer ImgPanels disappearing Imr1c2">
											<div class="inlineFolderPointer" style="left: 193.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divIllustrationContainer" id="divIllustrationContainer"></ul>
											</div>
										</section>
										<section class="framesContainer ImgPanels disappearing Imr1c3">
											<div class="inlineFolderPointer" style="left: 302.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divFramesContainer" id="divFramesContainer"></ul>
											</div>
										</section>
									</section>
									<ul class="gallery photoGallery albumGallery categories laidOut" style="height: 129px;">
										<li style="width: 109px; left: 0px; top: 0px;" class="ImgLr2c1"><a title="Banners" class="BtnBanners"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/grids.png" width="200" height="200" alt="Folder thumbnail" style="height: 81px;">
											<span class="text">Banners</span> </a></li>
										<li style="width: 109px; left: 109px; top: 0px;" class="ImgLr2c2"><a title="Shapes / Icons" class="btnShapes"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/shapes.png" width="200" height="200" alt="Folder thumbnail" style="height: 81px;">
											<span class="text">Shapes / Icons</span> </a></li>
										<li style="width: 109px; left: 218px; top: 0px;" class="ImgLr2c3"><a title="Logos" class="btnLogos"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/stickers.png" width="200" height="200" alt="Folder thumbnail" style="height: 81px;">
											<span class="text">Logos</span> </a></li>
									</ul>
									<section class="ImagesScrollContainer">
										<section class="bannersContainer ImgPanels disappearing Imr2c1">
											<div class="inlineFolderPointer" style="left: 84.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divBannersContainer" id="divBannersContainer"></ul>
											</div>
										</section>
										<section class="shapesContainer ImgPanels disappearing Imr2c2">
											<div class="inlineFolderPointer" style="left: 193.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divShapesContainer" id="divShapesContainer">
													<li class="DivCarouselImgContainerStyle2"><a href="#"><img src="assets-v2/btn-rectange.png" class="svg imgCarouselDiv draggable2 ui-draggable CustomRectangleObj" onclick="return fu15();" style="z-index:1000;" id="-21" /></a></li>
													<li class="DivCarouselImgContainerStyle2"><a href="#"><img src="assets-v2/btn-circle.png" class="svg imgCarouselDiv draggable2 ui-draggable CustomCircleObj" onclick="return fu16();" style="z-index:1000;" id="-22" /></a></li>
												</ul>
											</div>
										</section>
										<section class="logosContainer ImgPanels disappearing Imr2c3">
											<div class="inlineFolderPointer" style="left: 302.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divLogosContainer" id="divLogosContainer"></ul>
											</div>
										</section>
									</section>
									<ul class="gallery photoGallery albumGallery categories laidOut" style="height: 129px;">
										<li style="width: 109px; left: 0px; top: 0px;" class="ImgLr3c1"><a title="Template Images" class="btnTemplateImages"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/icons.jpg" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
											<span class="text">Template Images</span> </a></li>

									</ul>
									<section class="ImagesScrollContainer">
										<section class="templateImagesContainer ImgPanels disappearing Imr3c1">
											<div class="inlineFolderPointer" style="left: 84.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul id="divTempImgContainer" class="gallery infoPanelGallery photoGallery listDamImages divTempImgContainer"></ul>
											</div>
										</section>
									</section>
								</div>
							</div>

						</section>--%>
                </section>

                <section id="resultsLayout" class="results infinite twoCol">
                    <section class="layer  resultLayoutsScroller ">
                        <div class="inner">
                            <div class="searchGroup hasContent templateListULSG">
                                <div class="gallery infoPanelGallery templateGallery templateListUL">
                                </div>

                            </div>
                        </div>
                    </section>

                </section>
                <section id="resultsQText" class="results infinite">
                    <section class="layer scrollPane">
                        <div class="inner">
                            <ul class=" textControls">
                                <li><a class="add QuickTextTitle ThemeColor" style="" data-style="title">Update your Quick text Profile</a></li>
                            </ul>
                            <div class="QuickTextFields textControls">
                            </div>

                        </div>
                    </section>

                </section>
                <section id="pnlAddMain" class="pnlAddMain results">
                    <%--resultsText   class="results"--%>
                    <section class="layer scrollPaneImgDam">
                        <div class="inner browseCategories ">
                            <div class="AddBrowserCategories">
                                <ul class="gallery photoGallery albumGallery categories laidOut UlAddMain TempUlAddMain" id="TempUlAddMain" style="height: 258px;">
                                    <li style="width: 109px; left: 0px; top: 0px;" class="AddBLr1c1"><a title="Upload Images" id="uploadImagesMB"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <%--<img src="assets-v2/upload_folder_thumbnail.png" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />--%>
                                        <img src="" id="uploadImage" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Upload</span> </a></li>
                                    <li style="width: 109px; left: 109px; top: 0px;" class="AddBLr1c2"><a title="Add text" class="btnAtext"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="AddText" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Add Text</span> </a></li>
                                    <li style="width: 109px; left: 218px; top: 0px;" class="AddBLr1c3"><a title="Add Images" class="btnAFrames"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="Image" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Images</span> </a></li>
                                    <li style="width: 109px; left: 0px; top: 129px;" class="AddBLr1c3"><a title="Add Images" class="btnAShapes"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img src="assets-v2/shapes.png" width="200" height="200" class="ShapesDisplay" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Shapes</span> </a></li>
                                    <li style="width: 109px; left: 109px; top: 129px;" class="AddBLr1c3"><a title="CRM" class="btnACRM"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img src="assets-v2/crm.png" width="200" height="200" class="" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">CRM</span> </a></li>
                                    <li style="width: 109px; left: 218px; top: 129px;" class="AddBLr1c3"><a title="Text List" class="btnATextList"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img src="assets-v2/list.png" width="200" height="200" class="" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Text List</span> </a></li>
                                    <%--<li style="width: 109px; left: 0px; top: 258px;" class="AddBLr1c3"><a title="Upload Font" class="btnAUploadFont"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img src="assets-v2/uploadfont.png" width="200" height="200" class="" alt="Folder thumbnail" style="height: 81px;" />
                                        <span class="text">Upload Font</span> </a></li>--%>
                                </ul>
                            </div>
                            <section class="ImagesScrollContainer">
                                <section class="UploadsContainer AddPanels disappearing Addr1c1">
                                    <div class="inlineFolderPointer" style="left: 84.5px;"></div>
                                    <div class="inlineFolderGroup searchGroup hasContent" style="border-bottom: none;">
                                        <ul class="gallery infoPanelGallery photoGallery  " id="Ul1"></ul>
                                    </div>
                                </section>
                                <section class="Textcontainer AddPanels disappearing Addr1c2">
                                    <%--<div class="inlineFolderPointer" style="left: 193.5px;"></div>--%>
                                    <div class="inlineFolderGroup searchGroup hasContent" style="border-bottom: none;">
                                        <button class="btnBackFromTxt bkbtnIphone">Add</button>
                                        Back to Add
                                           <%-- <ul class="textControls">
                                                <li><a class="add addTxtHeading ThemeColor" id="a2" style="" data-style="title">Add text</a></li>
                                                <li><a class="add addTxtSubtitle ThemeColor" id="a3" style="" data-style="subtitle">Add subtitle text</a></li>
                                                <li><a class="add addTxtBody ThemeColor" id="a4" style="" data-style="body">Add a little bit of body text</a></li>
                                            </ul>--%>
                                        <ul class=" textControls">
                                            <li class="ThemeColor">
                                                <textarea name="txtareaAddTxt" id="txtareaAddTxt" placeholder="Add text to template" class="qTextInput" maxlength="500" style="height: 190px;"></textarea>

                                            </li>
                                            <li>
                                                <a class=" SampleBtn btntxt" id="btnAddheading">Add heading</a>
                                                <a class=" SampleBtn btntxt" id="btnAddSubtitle">Add as subtitle</a>
                                                <a class=" SampleBtn btntxt" id="btnaddbody">Add as body</a>
                                                <%--<a id="btnAddTxt" title="Save" style="width: 299px;" class="SampleBtn"><span class="onText">Add to canvas</span> </a>--%></li>
                                        </ul>
                                    </div>
                                </section>
                                <section class="ImagesContainer AddPanels disappearing Addr1c3">
                                    <div class="inlineFolderPointe r " style="left: 302.5px;"></div>
                                    <section class=" ">
                                        <div class="inner">
                                            <div class="inlineFolderGroup searchGroup hasContent browseCategories ImgsBrowserCategories " style="overflow: visible; border-bottom: none;">
                                                <div class="ulImagesSecTop">
                                                    <button class="btnBackFromImgs bkbtnIphone">Add</button>Back to Add
                                                    <ul class="gallery photoGallery albumGallery categories laidOut ulImagesSecTop" style="height: 129px;">
                                                        <li style="width: 109px; left: 0px; top: 0px;" class="ImgLr1c1"><a title="Free Images" class="btnFreeImgs"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                            <img id="FreeImages" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                            <span class="text">Free Images</span> </a></li>
                                                        <li style="width: 109px; left: 109px; top: 0px;" class="ImgLr1c2"><a title="Illustrations" class="btnIllustrations"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                            <img id="MyImages" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                            <span class="text">My Images</span> </a></li>
                                                        <li style="width: 109px; left: 218px; top: 0px;" class="ImgLr1c3"><a title="Frames" class="btnFrames"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                            <img id="MyLogos" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                            <span class="text">My Logos</span> </a></li>

                                                    </ul>
                                                </div>
                                                <section class="ImagesScrollContainer  ">
                                                    <section class="freeImgsContainer ImgPanels disappearing Imr1c1">
                                                        <%--<div class="inlineFolderPointer" style="left: 84.5px;"></div>--%>
                                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                                            <button class="btnBackGlImgs bkbtnIphone">Images</button>
                                                            Back to Images
                                                            <input type="text" placeholder="Search 1,000,000 images…" autocomplete="off" id="inputSearchTImg" class="searchBtn" />
                                                            <ul class="gallery infoPanelGallery photoGallery listDamImages divGlobImgContainer" id="divGlobImgContainer"></ul>
                                                        </div>
                                                    </section>
                                                    <section class="illustrationsContainer ImgPanels disappearing Imr1c2">
                                                        <%--  <div class="inlineFolderPointer" style="left: 193.5px;"></div>--%>
                                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                                            <button class="btnBackMyImg  bkbtnIphone">Images</button>Back to Images
                                                            <section class="uploadButtonAndMessage">
                                                                <button class="button buttonBlock fileUploadButton" id="uploadImages">Upload your own images / logos</button><p>Recommended 300 dpi CMYK JPeg or PDF files.</p>
                                                            </section>
                                                            <ul class="gallery infoPanelGallery photoGallery listDamImages divPersImgContainer" id="divPersImgContainer"></ul>
                                                        </div>
                                                    </section>
                                                    <section class="framesContainer ImgPanels disappearing Imr1c3">
                                                        <%--<div class="inlineFolderPointer" style="left: 302.5px;"></div>--%>
                                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                                            <button class="btnBackMyLogos bkbtnIphone">Images</button>Back to Images
                                                            <section class="uploadButtonAndMessage">
                                                                <button class="button buttonBlock fileUploadButton" id="uploadLogos">Upload your own logos</button><p>Recommended 300 dpi CMYK JPeg or PDF files.</p>
                                                            </section>
                                                            <ul class="gallery infoPanelGallery photoGallery listDamImages divPLogosContainer" id="divPLogosContainer"></ul>
                                                        </div>
                                                    </section>
                                                </section>
                                                <ul class="gallery photoGallery albumGallery categories laidOut ulImagesSecTop" style="height: 129px;">
                                                    <li style="width: 109px; left: 0px; top: 0px;" class="ImgLr3c1"><a title="Template Images" class="btnTemplateImages"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                        <img id="TemplateImages" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                        <span class="text">Template Images</span> </a></li>
                                                </ul>
                                                <section class="ImagesScrollContainer ">
                                                    <section class="templateImagesContainer ImgPanels disappearing Imr3c1">

                                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                                            <button class="btnBackTimgs bkbtnIphone">Images</button>Back to Images
                                                            <ul id="Ul5" class="gallery infoPanelGallery photoGallery listDamImages divTempImgContainer"></ul>
                                                        </div>
                                                    </section>
                                                </section>
                                            </div>
                                        </div>
                                    </section>
                                </section>
                                <section id="idShapesPanel" class="ImagesContainer AddPanels disappearing Addr1c4">
                                    <div class="inlineFolderPointe r " style="left: 302.5px;"></div>
                                    <section class=" ">
                                        <div class="inner">
                                            <div class="inlineFolderGroup searchGroup hasContent browseCategories ImgsBrowserCategories " style="overflow: visible; border-bottom: none;">
                                                <div class="ulImagesSecTop">
                                                    <button class="btnBackFromShapes bkbtnIphone">Add</button>Back to Add
                                                    <ul class="gallery photoGallery albumGallery categories laidOut ulImagesSecTop" style="height: 129px;">
                                                        <li style="width: 109px; left: 0px;" class="AddBLr1c3"><a title="Add Images" class="CustomRectangleObj"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                            <img id="btnAddRectangle" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                            <span class="text">Rectangle</span> </a></li>
                                                        <li style="width: 109px; left: 109px;" class="AddBLr1c3"><a title="Add Images" class="CustomCircleObj"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                                            <img id="btnAddCircle" src="" width="200" height="200" alt="Folder thumbnail" style="height: 81px;" />
                                                            <span class="text">Circle</span> </a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </section>
                            </section>

                        </div>
                    </section>

                </section>
                <section id="resultsBackground" class="results infinite">
                    <section class="layer bkDamScroller">
                        <div class="inner">
                            <%-- <input type="text" placeholder="Search 1,000,000 backgrounds…" autocomplete="off" id="inputSearchTBkg" class="searchBtn" />--%>


                            <div class="browseCategories bKimgBrowseCategories">
                                <ul class="gallery photoGallery albumGallery categories laidOut bkMainPanels" style="height: 129px;">
                                    <li style="width: 109px; left: 0px; top: 0px;" class="bkr1c1"><a title="Free Backgrounds" class="freeBackgrounds"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="FreeBackgrounds" src="" width="200" height="200" alt="Upload folder thumbnail" style="height: 81px;" /><span class="text">Free Backgrounds</span></a></li>
                                    <li style="width: 109px; left: 109px; top: 0px;" class="bkr1c2"><a title="My Backgrounds" class="myBackgrounds"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="IdMyBackgrounds" src="" width="200" height="200" alt="Facebook folder thumbnail" style="height: 81px;" /><span class="text">My Backgrounds</span></a></li>
                                    <li style="width: 109px; left: 218px; top: 0px;" class="bkr1c3"><a title="Template Backgrounds" class="templateBackgrounds"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="IdTemplateBackgrounds" src="" width="200" height="200" alt="Facebook folder thumbnail" style="height: 81px;" /><span class="text">Template Backgrounds</span></a></li>
                                </ul>
                                <section class="ImagesScrollContainer">
                                    <section class="freeBkImgsContainer BkImgPanels disappearing bkImr1c1">
                                        <%--<div class="inlineFolderPointer" style="left: 84.5px;"></div>--%>
                                        <button class="btnBkBkimgs bkbtnIphone">Backgrounds</button>Back to Backgrounds
                                        <br />
                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">

                                            <ul class="gallery infoPanelGallery photoGallery listDamImages listDambkimages divGlobBkImgContainer" id="divGlobBkImgContainer"></ul>
                                        </div>
                                    </section>
                                    <section class="myBkImgsContainer BkImgPanels disappearing bkImr1c2">
                                        <%--<div class="inlineFolderPointer" style="left: 193.5px;"></div>--%>
                                        <button class="btnBkmyBk bkbtnIphone">Backgrounds</button>Back to Backgrounds
                                        <br />
                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                            <ul class="gallery infoPanelGallery photoGallery listDamImages listDambkimages divPersBkImgContainer" id="divPersBkImgContainer"></ul>
                                        </div>
                                    </section>
                                    <section class="tempBackgroundImages BkImgPanels disappearing bkImr1c3">
                                        <%--<div class="inlineFolderPointer" style="left: 302.5px;"></div>--%>
                                        <button class="btnBkTempBk bkbtnIphone">Backgrounds</button>Back to Backgrounds
                                        <br />
                                        <div class="inlineFolderGroup searchGroup hasContent" style="border-top: none; padding-top: 0px;">
                                            <ul class="gallery infoPanelGallery photoGallery listDamImages listDambkimages divTempBkImgContainer" id="divTempBkImgContainer"></ul>
                                        </div>
                                    </section>
                                </section>
                                <ul class="gallery photoGallery albumGallery categories laidOut bkMainPanels" style="height: 129px;">
                                    <li style="width: 109px; left: 0px; top: 0px;" class="bkr2c1"><a title="Background colours" class="BkColors"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="IdBackgroundcolours" src="" width="200" height="200" alt="Background colours" style="height: 81px;" /><span class="text">Background  colours</span></a></li>
                                    <li style="width: 109px; left: 109px; top: 0px;" class="bkr2c2"><a title="Upload Backgrounds" class="" id="uploadBackgroundMn"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="IdUploadBackgrounds" src="" width="200" height="200" alt="Upload Backgrounds" style="height: 81px;" /><span class="text">Upload Backgrounds</span></a></li>
                                    <li style="width: 109px; left: 218px; top: 0px;" class="bkr2c3"><a title="Clear Backgrounds" class="" id="clearBackground"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
                                        <img id="ClearBackground" src="assets-v2/white.jpg" width="200" height="200" alt="Clear Backgrounds" style="height: 81px;" /><span class="text">Clear Background</span></a></li>
                                </ul>

                                <section class="ImagesScrollContainer">
                                    <section class="freeBkImgsContainer BkImgPanels disappearing bkImr2c1 ">
                                        <div class="inlineFolderPointer" style="left: 84.5px;"></div>
                                        <div class="inlineFolderGroup searchGroup hasContent" style="">
                                            <div class="">
                                                <div class="paletteToolbarWrapper static" style="display: block;">
                                                    <menu class="paletteToolbar colorToolBarBk">
                                                        <li class="colorOption" style="background-color: #020302"><a href="#" onclick="f2(0,0,0,100,'#020302','Black');">#020302</a></li>
                                                        <li class="colorOption" style="background-color: #ffffff"><a href="#" onclick='f2(0,0,0,0,"#ffffff");'>#ffffff</a></li>
                                                        <li class="colorOption" style="background-color: #EF4123"><a href="#" onclick='f2(0,88,100,0,"#EF4123","Orange 021");'>#EF4123</a></li>
                                                        <li class="colorOption" style="background-color: #FFF200"><a href="#" onclick="f2(0,100,100,0,'#FFF200','Yellow');">#FFF200</a></li>
                                                        <li class="colorOption" style="background-color: #04B14B"><a href="#" onclick="f2(75,0,100,0,'#04B14B','Green');">#04B14B</a></li>
                                                        <li class="colorOption" style="background-color: #034EA2"><a href="#" onclick='f2(100,82,0,2,"#034EA2","Reflex Blue");'>#034EA2</a></li>
                                                        <li class="colorOption" style="background-color: #EE2B76"><a onclick='f2(0,92,18,0,"#EE2B76","Pink 213");'>#EE2B76</a></li>
                                                        <li class="picker" style="display: inline-block;"><a id="AddBkColorRetailNew">Add a color</a></li>
                                                    </menu>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                    <section class="myBkImgsContainer BkImgPanels disappearing bkImr2c2">
                                        <div class="inlineFolderPointer" style="left: 193.5px;"></div>
                                        <div class="inlineFolderGroup searchGroup hasContent" style="">
                                            <ul class="gallery infoPanelGallery photoGallery listDamImages divPersBkImgContainer" id="Ul3"></ul>
                                        </div>
                                    </section>
                                    <section class="tempBackgroundImages BkImgPanels disappearing bkImr2c3">
                                        <div class="inlineFolderPointer" style="left: 302.5px;"></div>
                                        <div class="inlineFolderGroup searchGroup hasContent" style="">
                                            <section class="uploadButtonAndMessage">
                                                <button class="button buttonBlock fileUploadButton " id="uploadBackground">Upload your own backgrounds</button><p>Recommended 300 dpi CMYK JPeg or PDF files.</p>
                                                <section class="uploadButtonAndMessage">
                                                    <button class="button buttonBlock fileUploadButton ">Clear Background</button><p></p>
                                                </section>
                                            </section>
                                        </div>
                                    </section>
                                </section>

                            </div>
                            <div class="searchGroup" style="">
                                <ul class="gallery infoPanelGallery backgroundGallery"></ul>
                            </div>
                        </div>

                    </section>

                </section>
                <section id="resultsUpload" class="results infinite">
                    <section class="layer upDamScroller">
                        <div class="inner">
                            <%--    <input type="text" placeholder="Search your images…" autocomplete="off" id="inputSearchPImg" class="searchBtn" />--%>

                            <%--  <section class="uploadButtonAndMessage">
									<button class="button buttonBlock fileUploadButton">Upload your own logos</button><p>Recommended 300 dpi CMYK JPeg or PDF files.</p>
								</section>--%>
                            <%--								<div class="browseCategories browseCatUploads">
									<ul class="gallery photoGallery albumGallery categories laidOut" style="height: 129px;">
										<li style="width: 109px; left: 0px; top: 0px;" class="Upr1c1"><a title="Uploads" class="yourUploads"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/upload_folder_thumbnail.png" width="200" height="200" alt="Upload folder thumbnail" style="height: 81px;" /><span class="text">My Images</span></a></li>
										<li style="width: 109px; left: 109px; top: 0px;" class="Upr1c2"><a title="Your Logos" class="btnLogos"><span class="pageIMLoader on"></span><span class="pageIMLoader on"></span>
											<img src="assets-v2/infographics.png" width="200" height="200" alt="Facebook folder thumbnail" style="height: 81px;" /><span class="text">My Logos</span></a></li>
									</ul>
									<section class="ImagesScrollContainer">
										<section class="yourImagesContainer UpImgPanels disappearing UpImr1c1">
											<div class="inlineFolderPointer" style="left: 84.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divPersImgContainer" id="divPersImgContainer"></ul>
											</div>
										</section>
										<section class="yourLogosContainer UpImgPanels disappearing UpImr1c2">
											<div class="inlineFolderPointer" style="left: 193.5px;"></div>
											<div class="inlineFolderGroup searchGroup hasContent" style="">
												<ul class="gallery infoPanelGallery photoGallery listDamImages divPLogosContainer" id="divPLogosContainer"></ul>
											</div>
										</section>
									</section>
								</div>--%>
                        </div>
                    </section>

                </section>
                <section id="imageDetail" class="results infinite">
                    <section class="layer  scrollPane ">
                        <div class="inner">
                            <div class="searchGroup hasContent templateListULSG">
                                <div class="ImageContainer">
                                    <p><a class="add QuickTextTitle ThemeColor" style="" data-style="title">Image Information</a></p>

                                    <div class="pnlImgBank">
                                        <div class="ImgLbl" style="">Image Title</div>
                                        <input type="text" name="InputImgTitle" id="InputImgTitle" class="qTextInput" placeholder="Image Title" maxlength="500" />
                                    </div>
                                    <div class="pnlImgBank">
                                        <div class="ImgLbl" style="">Image Description</div>
                                        <input type="text" name="InputImgDescription" id="InputImgDescription" class="qTextInput" placeholder="Image Description" maxlength="500" />
                                    </div>
                                    <div class="pnlImgBank">
                                        <div class="ImgLbl" style="">Image </div>
                                        <img id="ImgDAMDetail" src="" />
                                    </div>

                                    <div class="pnlImgBank">
                                        <div class="ImgLbl" style="">Image Keywords</div>
                                        <textarea name="InputImgKeywords" id="InputImgKeywords" placeholder="Image Keywords" class="qTextInput" maxlength="500"></textarea>
                                    </div>
                                    <div class="divImageTypes">

                                        <div class="ImgLbl" style="">Image Type</div>
                                        <div class="radioBtnRow">
                                            <input id="radioImagePicture" type="radio" name="imageTypes" checked="checked" value="Picture" />
                                            <label for="radioImagePicture" id="Label2">
                                                Pictures</label>
                                        </div>
                                        <div class="radioBtnRow">
                                            <input id="radioImageLogo" type="radio" name="imageTypes" value="Logo" />
                                            <label for="radioImageLogo" id="Label4">
                                                Logos</label>
                                        </div>

                                        <div class="clearBoth"></div>
                                        <br />
                                    </div>
                                    <%--         <div class="" style="margin: 5px; clear: both; text-align: left;" id="territroyContainer">
											<div class="ImgLbl" style="">Select Territories</div>
											<div id="dropDownTerritories">
											</div>
										</div>--%>
                                    <div class="pnlImgBank" style="text-align: center;">
                                        <button id="btnUpdateImgProp" class="button">Update</button>
                                        <button id="btnDeleteImg" class="button">Delete</button>
                                        <br />
                                        <br />
                                        <p><a class="add QuickTextTitle ThemeColor returnToLib" style="" data-style="title">Return to Library</a></p>

                                    </div>

                                </div>

                            </div>
                        </div>
                    </section>
                </section>
                <section id="sectionLayers" class="results">
                    <%--id="sectionLayers" class="results infinite"--%>
                    <section class="layer scrollPane">
                        <div class="inner">
                            <ul class="textControls">
                                <li><a class=" QuickTextTitle ThemeColor" id="a1" style="font-size: 159%;">Manage template layers</a></li>
                            </ul>
                            <div id="LayerObjectsContainerRetail" class="searchGroup hasContent ">
                                <ul class="" id="sortableLayers">
                                </ul>

                            </div>

                        </div>
                    </section>

                </section>
                <section id="sectionPropertyPanels" class="results infinite">
                    <section class="layer  scrollPane "> <%--layerPropertyPanel--%>
                        <div class="inner">
                            <div class="searchGroup hasContent templateListULSG">
                                <div id="objPropertyPanel" class="ImageContainer">
                                    <div class="textPropertyPanel1">
                                        <div class="propertyPanelControlDiv imgthumbPreviewSlider" style="padding-left: 110px;">
                                            <img id="imgThumbPreview" height="80px" />

                                        </div>
                                        <div class="propertyPanelControlDiv" style="width: 303px; text-align: center;">
                                            <button id="btnReplaceImage" title="Delete Object" class="button  imgthumbPreviewSliderBtn">
                                                Replace Image
                                            </button>
                                            <%--<button id="btnDeleteImage" title="Delete Object" class="button  " style="width: 127px;">
                                                Crop Image
                                            </button>--%>
                                            <button id="BtnCropImg" title="Crop object" class="button" style="display: none; width: 127px;">
                                                Crop Image
                                            </button>
                                            <button id="BtnCropImg2" title="Crop object" class="button" style="width: 127px;">
                                                Crop Image
                                            </button>
                                        </div>
                                        <div class="DivBlockWithNoHeight vertLine"></div>

                                        <div class="textPropertyPanel2" style="padding-top: 20px;">
                                            <div class="propertyPanelControlDiv">
                                                <button id="AddColorShape" class="BtnChngeClr" title="Colour picker">Color
                                                </button>
                                                <div class="inputObjectAlphaSlider"></div>
                                                <button id="BtnImgRotateRight" class="BtnRotateRight" title="Rotate Right">
                                                </button>
                                                
                                                <button id="BtnImgRotateLeft" class="BtnRotateLeft" title="Rotate Left">
                                                </button>
                                            </div>

                                            <div class="propertyPanelControlDiv" style="margin-top:30px">
                                                
                                                <div class="divTxtAlignmentControls">
                                                    
                                                   <label class="textLabel" >Align to Canvas</label>
                                                    <button id="btnImgCanvasAlignLeft" class="btnCj1" title="">
                                                    </button>
                                                    <button id="BtnImgCanvasAlignCenter" class="btnCj2" title="">
                                                    </button>
                                                    <button id="BtnImgCanvasAlignMiddle" class="btnCj5" title="">
                                                    </button>
                                                    <button id="BtnImgCanvasAlignRight" class="btnCj3" title="">
                                                    </button>
                                                </div>

                                            </div>
                                            <div class="DivBlockWithNoHeight vertLine"></div>
                                        </div>
                                        <%-- ---------------------------------------------%>



                                       
                                            <div class="propertyPanelControlDiv">
	<div class="PostionPanelLabel">
		<span class="lblcheckbox">Position</span>
	</div>
	<div class="PostionPanelLabel">
		<span class="lblcheckbox" id="lblArrangeOrderiImg">Box Size</span>
	</div>
	<div class="PostionPanelLabel">
		<div class="lblcheckbox" id="">Lock Position </div>
	</div>
</div>
<div class="propertyPanelControlDiv">
	<div class="margin10" style="padding-bottom:10px">
	<div class="lbltextButtonControl">

	   <span class="paddingright5">X</span>
	   <input name="inputPositionXTxt" id="inputPositionX" class="width28 zindex100" />
		
		</div>
	<div class="lbltextButtonControl">
		<span class="paddingright5">W </span>
		 <input name="inputPositionYTxt" id="inputPositionY" class="width28 zindex100" />
		
	</div>
		</div>
	<div class="margin10">
	<div class="lbltextButtonControl" style="float: left;">

		<span class="paddingright5">Y</span>
		<input name="inputObjectWidthTxt" id="inputObjectWidth" class="width28 zindex100" />
		  </div>
		 <div class="lbltextButtonControl" style="float: left;">

		<span class="paddingright5" style="padding-left:4px;">H</span>
		<input name="inputObjectHeightTxt" id="inputObjectHeight" class="width28 zindex100" />


	</div>
		</div>
	<div class="PostionPanelLabel">
		<div class="lbltextButtonControl">
			<div class="onoffswitch"  style="top: -20px; margin-left:-3px">
				<input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="LockPositionImg" checked="">
				<label class="onoffswitch-label" for="LockPositionImg" id="lblLockPositionImg">
					<div class="onoffswitch-inner"></div>
					<div class="onoffswitch-switch"></div>
				</label>
			</div>
		</div>


	</div>
</div>
            <div class="DivBlockWithNoHeight vertLine"></div>
                                            
                                                                            <%--<div class="margin10">
                                                <div class="CheckBoxlabel" id="">Lock Position  </div>
                                                <div class="onoffswitch">

                                                    <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="LockPositionImg" checked="">
                                                    <label class="onoffswitch-label" for="LockPositionImg" id="lblLockPositionImg">
                                                        <div class="onoffswitch-inner"></div>
                                                        <div class="onoffswitch-switch"></div>
                                                    </label>
                                                </div>
                                                <div class="PostionPanelLabel">
                                                    <span class="largeText RotateLbl3 marginLeft15 lblArrangeOrderTxt">Box Size</span>
                                                </div>
                                            </div>--%>
                                          
                                                
                                                <%--<div class="PostionPanelLabel">
                                                    <span class="marginLeft40 marginRight5 largeText">W </span>
                                                    <input name="inputObjectWidthTxt" id="inputObjectWidth" class="width28 zindex100" />
                                                </div>--%>
                                          <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Make Overlay Layer  </div>
                                        <div class="onoffswitch" style="right: -99px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="chkboxOverlayImg" checked="">
                                            <label class="onoffswitch-label" for="chkboxOverlayImg">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="propertyPanelControlDiv">
                                        <div class="lblchckboxControl" id="">Print Object On PDF </div>
                                        <div class="onoffswitch" style="right: -98px">
                                             <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="BtnPrintImage" checked="">
                                            <label class="onoffswitch-label" for="BtnPrintImage" id="lblBtnPrintImage">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>


                                           
                                             <div class="DivBlockWithNoHeight vertLine"></div>
                      <%--                     <div class="CheckBoxlabel" id="">Allow End User to CHANGE Formatting</div>
                                                <div class="onoffswitch" style="right: -124px">

                                                    <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="LockImgProperties" checked="">
                                                    <label class="onoffswitch-label" for="LockImgProperties" id="lblLockImgProperties">
                                                        <div class="onoffswitch-inner"></div>
                                                        <div class="onoffswitch-switch"></div>
                                                    </label>
                                                </div>--%>
                                             <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Allow End User to CHANGE Formatting </div>
                                        <div class="onoffswitch" style="right: -3px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="LockImgProperties" checked="">
                                            <label class="onoffswitch-label" for="LockImgProperties" id="lblLockImgProperties">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>



                                          <%--  <div class="marginXY">

                                                <div class="PostionPanelLabel">
                                                    <span class="marginLeft40 marginRight5 largeText">X</span>
                                                    <input name="inputPositionXTxt" id="inputPositionX" class="width28 zindex100" />
                                                </div>
                                            </div>
                                            <div class="marginXY">

                                                <div class="PostionPanelLabel">

                                                    <span class="marginLeft40 marginRight5 largeText">Y</span>
                                                    <input name="inputPositionYTxt" id="inputPositionY" class="width28 zindex100" />
                                                </div>
                                            </div>--%>
                                       
                                        <%--  ----------------------------%>
                                        <%--     <div class="propertyPanelControlDiv">
                                            <p class="largeText RotateLbl3" id="P2">
                                                Scale
                                            </p>
                                            <button id="BtnImgScaleIN" title="Scale in">
                                            </button>
                                            <button id="BtnImgScaleOut" title="Scale out">
                                            </button>
                                        </div>
                                        <div class="propertyPanelControlDiv">
                                            <div class="imgtool">
                                                <p class="largeText RotateLbl3" id="LbImgCrop">
                                                    Crop
                                                </p>
                                                <button id="BtnCropImg" title="Crop object" style="display: none;">
                                                </button>
                                                <button id="BtnCropImg2" title="Crop object">
                                                </button>
                                            </div>
                                            <div class="shapeTools">
                                                <p id="AddColorShapeLbl" class="largeText RotateLbl3">
                                                    Colour
                                                </p>
                                                <button id="AddColorShape" class="" title="Colour picker">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="propertyPanelControlDiv">
                                            <br />
                                        </div>--%>
                                    </div>
                                    <div class="textPropertyPanel2">
                                        <div class="propertyPanelControlDiv">
                                            <%--<span class="largeText RotateLbl3" id="spanLblTransparency">Opacity</span>
												<input name="inputObjectAlpha" id="inputObjectAlpha" class="width28 zindex100" />--%>
                                            <%--<span class="marginRight5 largeText">W </span>
                                            <input name="inputObjectWidth" id="inputObjectWidth" class="width28 zindex100" />
                                            <span class="marginLeft15 marginRight7 largeText">H</span>
                                            <input name="inputObjectHeight" id="inputObjectHeight" class="width28 zindex100" />
                                        </div>
                                        <div class="propertyPanelControlDiv largeText" style="padding-bottom: 0px; padding-top: 20px;">
                                            <span style="margin-left: 4px;">To front </span><span style="margin-left: 23px;">+
                                            </span><span style="margin-left: 36px;">- </span><span style="margin-left: 22px;">To
									back </span>
                                        </div>
                                        <div class="propertyPanelControlDiv" style="padding-top: 20px;">
                                            <button id="BtnImageArrangeOrdr1" title="Bring object to front">
                                            </button>
                                            <button id="BtnImageArrangeOrdr2" title="Bring object a step front">
                                            </button>
                                            <button id="BtnImageArrangeOrdr3" title="Send object a step back">
                                            </button>
                                            <button id="BtnImageArrangeOrdr4" title="Send object to back">
                                            </button>
                                            <%--	<button id="BtnImgRotateLeft" title="Rotate object left">
												</button>
												<button id="BtnImgRotateRight" title="Rotate object right">
												</button>
                                        </div>
                                       <%-- <div class="propertyPanelControlDiv" style="padding-top: 20px;">
                                            <label style="width: 60px">Opacity</label>
                                            <div class="inputObjectAlphaSlider"></div>
                                        </div>
                                        <div class="propertyPanelControlDiv" style="padding-top: 20px;">
                                            <label style="width: 60px">Rotate</label>
                                            <div class="rotateSlider" style="margin-left: 22px;"></div>
                                        </div>
                                        <div class="DivBlockWithNoHeight vertLine" style="width: 313px;"></div>
                                        <div class="propertyPanelControlDiv">
                                           <%-- <div class="divTxtAlignmentControls">
                                                <button id="btnImgCanvasAlignLeft" class="btnCj1" title="">
                                                </button>
                                                <button id="BtnImgCanvasAlignCenter" class="btnCj2" title="">
                                                </button>
                                                <button id="BtnImgCanvasAlignMiddle" class="btnCj5" title="">
                                                </button>
                                                <button id="BtnImgCanvasAlignRight" class="btnCj3" title="">
                                                </button>
                                            </div>--%>
                                            <%--<div class="divTxtPositioningCotnrols">
                                                <span class="marginLeft15 marginRight5" style="margin-right: 25px;">X </span>
                                                <input name="inputPositionX" id="inputPositionX" class="width28 zindex100" />
                                                <br />
                                                <br />
                                                <span class="marginLeft15 marginRight5" style="margin-right: 25px;">Y </span>
                                                <input name="inputPositionY" id="inputPositionY" class="width28 zindex100" />

                                            </div>
                                            <div class="clearBoth">
                                            </div>--%>
                                        </div>
                                        <%--  <div class="DivBlockWithNoHeight vertLine" style="width: 313px; margin-top: 10px;"></div>
                                        <div class="propertyPanelControlDiv" style="width: 303px; text-align: center;">
                                            <button id="btnReplaceImage" title="Delete Object" class="button  
                                            SliderBtn">
                                                Replace Image
                                            </button>
                                            <button id="btnDeleteImage" title="Delete Object" class="button  " style="width: 127px;">
                                                Remove
                                            </button>
                                            <p style="text-align: center; margin-top: 12px;">
                                                <a class="add QuickTextTitle ThemeColor returnToLayers" style="" data-style="title">Return to Layers</a>
                                            </p>
                                        </div>--%>
                                    </div>
                                </div>
                                <div id="textPropertyPanel" class="ImageContainer">
                                    <div class="textPropertyPanel1">
                                        <div class="propertyPanelControlDiv" style="padding-top: 10px;">
                                            <%--<label class="largeText  lbtTxtArea" for="EditTXtArea">
                                                Dobule click text object on canvas to modify text. </label>--%>

                                            <label class="largeText  lblEditTxtTxt" for="">
                                            </label>
                                        </div>
                                        <div class="propertyPanelControlDiv">
                                            <select id="BtnSelectFonts" title="Select Font" class="styledBorder" >
                                                <option value="(select)">(select)</option>
                                            </select>
                                            

                                        </div>
                                        <div class="propertyPanelControlDiv" style="padding-top: 14px;">
                                            
                                            <%--<button id="BtnBoldTxt" class="" title="Bold">
                                            </button>
                                            <button id="BtnItalicTxt" title="Italic">
                                            </button>--%>
                                            <img id="BtnBoldTxt" class="fontawesomeIcons" src="assets-v2/bold.png" title="Bold" />
                                             <img id="BtnItalicTxt" class="fontawesomeIcons"  src="assets-v2/italic.png" title="Italic" />
                                            <img id="BtnUnderlineTxt" class="fontawesomeIcons"  src="assets-v2/underline.png" title="Underline" />
                                             <img id="BtnBulletedLstTxt" class="fontawesomeIcons"  src="assets-v2/list.png" title="Bulleted List" />
                                            <span class="selectFontSize">
                                                <input id="BtnFontSize" style="" class="inputCharSpacing zindex100"/>
                                            </span>
                                           <span>
                                               <button id="BtnIntrnationalkybrd"  title="international keyboard selection">
		                                            <div class="XLText">UK </div>
		                                            <div class="smallText">Keyboard </div>
	                                            </button>
                                           </span>
	                                            
                                           
                                             </div>
                                          <div class="propertyPanelControlDiv" >
                                            <img id="BtnJustifyTxt1" title="Justify text left" class="fontawesomeIcons"  src="assets-v2/align1.png"/>
                                      
                                            <img id="BtnJustifyTxt2" title="Justify text center" class="fontawesomeIcons"  src="assets-v2/align2.png"/>
                                          
                                            <img id="BtnJustifyTxt3" title="Justify text right" class="fontawesomeIcons"  src="assets-v2/align3.png"/>
                                          
                                            <img src="assets/imgCharSpace.png" class="iconsPropPanel imgLineHeight" />
                                            <input name="inputcharSpacing" id="inputcharSpacing" class="textButtonControl inputCharSpacing zindex100" />
                                             

                                            <%--<button id="BtnUploadFont" title="Upload Font" class="button">
                                                Upload Font
                                            </button>--%>


                                        </div>
                                         <div class="propertyPanelControlDiv" >
                                            <img id="BtnValignTxt1" title="Align top" class="fontawesomeIcons"  src="assets-v2/valign3.png"/>
                                      
                                            <img id="BtnValignTxt2" title="Align center" class="fontawesomeIcons"  src="assets-v2/valign2.png"/>
                                          
                                            <img id="BtnValignTxt3" title="Align Bottom" class="fontawesomeIcons"  src="assets-v2/valign1.png"/>
                                          
                                           <img src="assets/lineHeight.png" class="iconsPropPanel imgLineHeight" style="padding-left: 2px;" />
                                            <input name="txtLineHeight" id="txtLineHeight" class="textButtonControl inputCharSpacing zindex100" />
                                            
                                              <button id="BtnChngeClr" class="BtnColorPicker BtnChngeClr" title="Colour picker">
                                                Color
                                            </button>
                                       </div>

                                        <div class="propertyPanelControlDiv" style="margin-top: 15px;">

                                             
                                            <button id="BtnVarification" title="Upload Font" class="button">
                                                Send CRM Verification 
                                            </button>
                                           <button id="BtnRotateTxtRight" class="BtnRotateRight" title="Rotate Right">
                                            </button>
                                           <button id="BtnRotateTxtLft" class="BtnRotateLeft" title="Rotate Left">
                                            </button>

                                        </div>
                                      
                                        <%--	<div id="AddColorTxtRetailDiv" class="propertyPanelControlDiv" style="padding-top: 20px;">
												<div class="ColorPallet" style="background-color: #ED1C24" onclick="f2(0,100,100,0,&quot;#ED1C24&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #F5821F" onclick="f2(0,60,100,0,&quot;#F5821F&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #FFF200" onclick="f2(0,0,100,0,&quot;#FFF200&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #1E9860" onclick="f2(75,0,75,20,&quot;#1E9860&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #00AEEF" onclick="f2(100,0,0,0,&quot;#00AEEF&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #4C4C4E" onclick="f2(0,0,0,85,&quot;#4C4C4E&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #FFFFFF" onclick="f2(0,0,0,0,&quot;#FFFFFF&quot;,&quot;null&quot;);"></div>
												<div class="ColorPallet" style="background-color: #A7A9AC" onclick="f2(0,0,0,40,&quot;#A7A9AC&quot;,&quot;null&quot;);"></div>
												<button id="AddColorTxtRetail" class="" title="Colour picker">
													MORE
												</button>
											</div>--%>
                                    </div>


                                    <div class="textPropertyPanel2" style="padding-top: 20px;">

                             
                                        <div class="propertyPanelControlDiv">
                                            <%--<div class="textLabel">
                                                     
                                                </div>--%>
                                            <div class="divTxtAlignmentControls">
                                                <label class="textLabel">Align to Canvas</label>
                                                <button id="BtnTxtCanvasAlignLeft" class="btnCj1" title="">
                                                </button>
                                                <button id="BtnTxtCanvasAlignCenter" class="btnCj2" title="">
                                                </button>
                                                <button id="BtnTxtCanvasAlignMiddle" class="btnCj5" title="">
                                                </button>
                                                <button id="BtnTxtCanvasAlignRight" class="btnCj3" title="">
                                                </button>
                                            </div>
                                            <%-- <div class="divTxtPositioningCotnrols">
                                                <span class="marginLeft15 marginRight5" style="margin-right: 25px;">X </span>
                                                <input name="inputPositionXTxt" id="inputPositionXTxt" class="width28 zindex100" />
                                                <br />
                                                <br />
                                                <span class="marginLeft15 marginRight5" style="margin-right: 25px;">Y </span>
                                                <input name="inputPositionYTxt" id="inputPositionYTxt" class="width28 zindex100" />

                                            </div>
                                            <div class="clearBoth">--%>
                                        </div>
                                    
                                        <div class="DivBlockWithNoHeight vertLine"></div>
                                    </div>
                                    <div class="propertyPanelControlDiv">

                                        <div class="PostionPanelLabel">
                                            <span class="lblcheckbox">Position</span>
                                        </div>
                                        <div class="PostionPanelLabel">
                                            <span class="lblcheckbox" id="lblArrangeOrderTxt">Box Size</span>
                                        </div>
                                        <div class="PostionPanelLabel">
                                            <div class="lblcheckbox" id="">Lock Position </div>
                                        </div>
                                    </div>
                                    <div class="propertyPanelControlDiv">
                                        <div class="margin10" style="padding-bottom:10px">
                                        <div class="lbltextButtonControl">

                                           <span class="paddingright5">X</span>
                                            <input name="inputPositionXTxt" id="inputPositionXTxt" class="textButtonControl zindex100" />
                                            </div>
                                        <div class="lbltextButtonControl">
                                            <span class="paddingright5">W </span>
                                            <input name="inputObjectWidthTxt" id="inputObjectWidthTxt" class="textButtonControl zindex100" />
                                        </div>
                                            </div>
                                        <div class="margin10">
                                        <div class="lbltextButtonControl" style="float: left;">

                                            <span class="paddingright5">Y</span>
                                            <input name="inputPositionYTxt" id="inputPositionYTxt" class="textButtonControl zindex100" />
                                              </div>
                                             <div class="lbltextButtonControl" style="float: left;">

                                            <span class="paddingright5" style="padding-left:4px;">H</span>
                                            <input name="inputObjectHeightTxt" id="inputObjectHeightTxt" class="textButtonControl zindex100" />


                                        </div>
                                            </div>
                                        <div class="PostionPanelLabel">
                                            <div class="lbltextButtonControl">
                                                <div class="onoffswitch"  style="top: -20px;margin-left:3px;">
                                                    <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="BtnLockTxtPosition" checked="">
                                                    <label class="onoffswitch-label" for="BtnLockTxtPosition">
                                                        <div class="onoffswitch-inner"></div>
                                                        <div class="onoffswitch-switch"></div>
                                                    </label>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="DivBlockWithNoHeight vertLine"></div>
                                    <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Make Overlay Layer  </div>
                                        <div class="onoffswitch" style="right: -100px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="chkboxOverlayTxt" checked="">
                                            <label class="onoffswitch-label" for="chkboxOverlayTxt">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="propertyPanelControlDiv">
                                        <div class="lblchckboxControl" id="">Print Object On PDF </div>
                                        <div class="onoffswitch" style="right: -99px">
                                             <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="BtnPrintObj" checked="">
                                            <label class="onoffswitch-label" for="BtnPrintObj">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                     <div class="DivBlockWithNoHeight vertLine"></div>
                                     <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Allow End User to CHANGE Formatting </div>
                                        <div class="onoffswitch" style="right: -4px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="BtnAllowOnlyTxtChange" checked="">
                                            <label class="onoffswitch-label" for="BtnAllowOnlyTxtChange">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>

                                      <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Allow End User to EDIT text </div>
                                        <div class="onoffswitch" style="right: -61px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="BtnLockEditing" checked="">
                                            <label class="onoffswitch-label" for="BtnLockEditing">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="propertyPanelControlDiv">
                                        <div class="lblchckboxControl" id="">Auto Shrink Text to fit in Text Box </div>
                                        <div class="onoffswitch" style="right:-31px">
                                            <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="chkboxAutoShrink" checked="">
                                            <label class="onoffswitch-label" for="chkboxAutoShrink">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="DivBlockWithNoHeight vertLine"></div>
                                     <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Text Input Mask </div>
                                        <div class="onoffswitch" style="right: -118px">
                                              <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="chkboxTxtInputMask" checked="">
                                            <label class="onoffswitch-label" for="chkboxTxtInputMask">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                     <div class="propertyPanelControlDiv" style="padding-bottom:5px">
                                        <div class="lblchckboxControl" id="">Value in This Mandatory </div>
                                        <div class="onoffswitch" style="right:-77px">
                                              <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="chkboxTxtInputMandatory" checked="">
                                            <label class="onoffswitch-label" for="chkboxTxtInputMandatory">
                                                <div class="onoffswitch-inner"></div>
                                                <div class="onoffswitch-switch"></div>
                                            </label>
                                        </div>
                                    </div>
                                   
                                   
                                     <div class="propertyPanelControlDiv">
                                        <div class="lblchckboxControl" id=""># Number @ Letters -Or Type Mandatory Characters </div>
                                        <div  style="right: -2px">
                                            <input type="text" id="txtInputMskCntrl"  />
                                        </div>
                                    </div>
                                   <div class="CaseModeSlider"> </div>
                                    <div >
                                        <div class="lblCaseModeSlider" id="">Normal </div>
                                        <div class="lblCaseModeSlider" id="">Upper Case </div>
                                        <div class="lblCaseModeSlider" id="">Lower Case </div>
                                        <div class="lblCaseModeSlider" id="">Sentence Format </div>

                                    </div>
                               
                                   
                                    
                                </div>
                            

                                <%--<div class="DivBlockWithNoHeight vertLine" style="width: 313px;"></div>--%>

                                <%-- <div class="DivBlockWithNoHeight vertLine" style="width: 313px; margin-top: 10px;"></div>
                                        <div class="propertyPanelControlDiv" style="width: 303px;">

                                          <button id="BtnDeleteTxtObj" title="Delete text" class="button buttonBlock ">
                                                Remove
                                            </button>
                                            <p style="text-align: center; margin-top: 12px;">
                                                <a class="add QuickTextTitle ThemeColor returnToLayers" style="" data-style="title">Return to Layers</a>
                                            </p>
                                        </div>--%>
                            </div>

                        </div>

                        </div>
                    </section>
                </section>
                <section id="sectionLayouts" class="results infinite">
                    <section class="layer scrollPane">
                        <div class="inner">
                            <ul class=" textControls">
                                <li><a class=" QuickTextTitle ThemeColor" style="" data-style="title">Layouts</a></li>
                            </ul>
                            <div class="divLayoutBtnContainer"></div>
                        </div>
                    </section>

                </section>
            </section>
            <div id="FrontBackOptionPanal" style="display: none">
                <section id="FrontBackOptionPanalSection" class="TextObjectPropertyPanal">

                    <div class="TextObjectPropertyPanel">
                        
                        <div>
                            <button id="BtnDeleteTxtObj" class="RemoveButton">Remove </button>
                        </div>
                        <div>
                            <button id="BtnTxtarrangeOrder1" title="Bring text to front" class="">To Front</button>
                        </div>
                        <div>
                            <button id="BtnTxtarrangeOrder2" title="Bring text a step front" class="">+ </button>
                        </div>
                        <div>
                            <button id="BtnTxtarrangeOrder3" title="Send text a step back" class="">- </button>
                        </div>
                        <div>
                            <button id="BtnTxtarrangeOrder4" title="Send text to back" class="">To Back</button>
                        </div>
                        <div>
                            <button id="" class="HelpButton">Help</button>
                        </div>

                    </div>
                    <%-- <div class="propertyPanelControlDiv largeText" style="padding-bottom: 0px; padding-top: 20px;">
                                            <span style="margin-left: 4px;" id="lblTxtArrange4">To front </span><span style="margin-left: 23px;">+
                                            </span><span style="margin-left: 36px;">- </span><span style="margin-left: 22px;" id="lblTxtArrange1">To
									back </span>
                                        </div>
                                        <div class="propertyPanelControlDiv" style="padding-top: 20px;">
                                            <button id="BtnTxtarrangeOrder1" title="Bring text to front">
                                            </button>
                                            <button id="BtnTxtarrangeOrder2" title="Bring text a step front">
                                            </button>
                                            <button id="BtnTxtarrangeOrder3" title="Send text a step back">
                                            </button>
                                            <button id="BtnTxtarrangeOrder4" title="Send text to back">
                                            </button>
                                           
                                        </div>--%>
                </section>
                <span></span>
                <%--<span></span>--%>
            </div>

        </section>
        <section id="canvaDocument" class="scrollPane2 multiPage" style="left: 430px;">

            <section id="canvasSection" class="page selected on" style="width: 326.9368785071841px; height: 462.4056858483221px; top: 136px; left: 20px;">
                <canvas id="canvas">Your browser does not support the canvas.Please upgrade to latest browser supporting HTML5. 
                </canvas>
                <%--  <menu class="pageToolbar vertical">
						<li class="pageToolbarUp disabled"><a title="Move page up">Move page up</a></li>
						<li class="pageToolbarDown enabled"><a title="Move page down">Move page down</a></li>
						<li class="pageToolbarCopy enabled"><a title="Copy this page">Copy this page</a></li>
						<li class="pageToolbarDelete enabled"><a title="Delete this page">Delete this page</a></li>

					</menu>--%>
            </section>
            <%--     <a id="addNewPage" title="Add a new page" style="top: 575.405685848322px; left: 274px; width: 327px;"><span class="onText">+ Add a new page</span> </a>--%>
        </section>

        <section id="editorControls">
            <a id="editorLogo" href="#" class=""><span id="editorLogoImage">Proof</span></a>
          <%--  <div>
                <menu id="Menu1" class="documentMenu documentCopyPaste">
                    
                    

                </menu>
            </div>--%>
            <div id="documentTitleAndMenu" class="">
                <menu id="mainTopMenu" class="documentMenu">
                    <li id="Li2" class="zoomBtns">
                        <button class="button" id="zoomOut" title="Zoom out">-</button></li>
                    <li class="listText">Zoom</li>
                    <li id="Li1" class="zoomBtns" style="margin-right:25px;">
                        <button class="button" id="zoomIn" title="Zoom In">+</button></li>
                    
                    <li id="documentMenuCopy">
                        <button class="button" id="btnMenuCopy" title="Copy"></button>
                    </li>
                    <li id="documentMenuPaste">
                        <button class="button" id="btnMenuPaste" title="Paste"></button>
                    </li>
                    <li id="documentMenuUndo">
                        <button class="button" id="BtnUndo" title="Undo"></button>
                    </li>
                    <li id="documentMenuRedo">
                        <button class="button" id="BtnRedo" title="Redo"></button>
                    </li>
                    <li id="documentMenuGuides">
                        <button class="button" id="BtnGuides" title="Show / Hide Trim Line"></button>
                    </li>
                </menu>
                <menu id="documentMenu" class="documentMenu dark" style="top:70px!important">
                    <span class="marker" style="left: 0px; width: 72px;"></span>
                </menu>

                <%--<h1 id="documentTitle" spellcheck="false" title="Click to rename your design">
                    <input type="text" value="Untitled design" style="width: 300px;" id="txtTemplateTitle" />
                </h1>--%>
            </div>

            <%--<a id="documentMenuHelp" href="/" target="_blank" class="">Need help?</a>--%>
             <section id="documentTitleName" class="">
                 <h1 id="documentTitle" spellcheck="false" title="Click to rename your design">
                    <input type="text" value="Untitled design" style="width: 300px;" id="txtTemplateTitle" />
                </h1>
                  <div class="dimentionsContainer">
                        <span class="dimentionsBC"></span>
                    </div>
               <%--<a id="zoomIn" title="Zoom in">+</a>
					<span id="zoomText">100%</span>
					<a id="zoomOut" title="Zoom out">–</a>--%>
            </section>
                
        </section>

    </section>
    <section id="MainLoader" class="dialogContainer dialogProof">
        <div class="mask on"></div>
        <div class="dialog" style="top: 93.5px; height: 184px;">
            <h2>Preparing your Template…</h2>
            <div class="content">
                <div class="exporting">
                    <div class="progress"><span class="progressValue" style="width: 1%;"><span class="text">95%</span><span class="inner"></span></span></div>
                    <p id="paraLoaderMsg">You can add different layouts to your design, you can also add images, text content,  background colour and image to your design. </p>
                </div>
            </div>
        </div>
    </section>
    <section class="menusContainer">
        <div id="divTxtPropPanelRetail" class=" divTxtPropPanelRetail retailPropPanels " style="display: none;">
            <menu class="tooltipToolbar textToolbar realToolbar txtMenuDiv firstMenuDiv">
                <li class="textToolbarFont">
                    <select id="BtnSelectFontsRetail" title="Select Font" class="styledBorder retailStoreSelect">
                        <option value="">(select)</option>
                    </select>
                    <input id="BtnFontSizeRetail" style="" />
                </li>
                <li class="textToolbarColor">
                    <a id="AddColorTxtRetailNew" class="pickAColor" href="#" title="Pick a color">
                        <span style="background-color: orange;" class="spanTxtcolour">Pick a color</span>
                    </a>
                </li>
                <li class="elementToolbarDelete enabled"><a title="Delete" id="btnDeleteTxt">Delete</a></li>
                <li class="textToolbarMore "><a onclick="pcL02_main();">More</a></li>
            </menu>
            <menu class="toolbarMoreGroup toolbarReferencePoint downwards on txtMenuDiv secondMenuDiv toolbarText" style="top: 42px; left: 340px; display: none;">
                <li id="BtnBoldTxtRetail" class="textToolbarBold enabled"><a>Bold</a></li>
                <li id="BtnItalicTxtRetail" class="textToolbarItalic enabled"><a>Italic</a></li>
                <li id="BtnJustifyTxt1Retail" class="textToolbarLeft enabled on"><a href="#">Left –</a></li>
                <li id="BtnJustifyTxt2Retail" class="textToolbarCenter enabled"><a href="#">– Center –</a></li>
                <li id="BtnJustifyTxt3Retail" class="textToolbarRight enabled"><a>– Right</a></li>
                <li id="BtnCopyObjTxtRetail" class="dividerAbove elementToolbarCopy enabled"><a>Copy</a></li>
                <li id="BtnRotateTxtLftRetail" class="elementToolbarLink enabled AnchorRotateLeft"><a>Rotate left</a></li>
                <li id="BtnRotateTxtRightRetail" class="elementToolbarLink enabled AnchorRotateRight"><a>Rotate right</a></li>
                <li id="BtnTxtarrangeOrder1Retail" class="elementToolbarLink enabled AnchorMoveUp" style="margin-top: 0px;"><a>Bring forward</a></li>
                <li id="BtnTxtarrangeOrder2Retail" class="elementToolbarLink enabled AnchorMoveUp" style="margin-top: 0px;"><a>Forward</a></li>
                <li id="BtnTxtarrangeOrder3Retail" class="elementToolbarLink enabled AnchorMoveDown" style="margin-top: 0px;"><a>Back</a></li>
                <li id="BtnTxtarrangeOrder4Retail" class="elementToolbarLink enabled AnchorMoveDown" style="margin-top: 0px;"><a>Send back</a></li>
            </menu>


        </div>

        <div id="divImgPropPanelRetail" class="imageToolbar divTxtPropPanelRetail retailPropPanels " style="display: none;">
            <menu class="tooltipToolbar elementToolbar " id="">


                <li class="elementToolbarCopy "><a id="BtnCopyObjImgRetail" title="Copy">Copy</a></li>
                <li class="elementToolbarForward "><a id="BtnImageArrangeOrdr2Retail" title="Forward">Forward</a></li>
                <li class="elementToolbarBackward "><a id="BtnImageArrangeOrdr3Retail" title="Back">Back</a></li>
                <li class="elementToolbarDelete enabled"><a id="btnDelImgRetail" href="#" title="Delete">Delete</a></li>
                <li class="elementToolbarCrop  elementCrop"><a id="BtnCropImgRetail" href="#" title="Crop">Crop</a></li>
                <li class="elementToolbarColor elementColorImg"><a id="AddColorImgRetailNew" class="pickAColor" href="#" title="Pick a color">
                    <span class="spanRectColour" style="background-color: rgb(0, 0, 0);">Pick a color</span></a></li>

                <li class="elementToolbarMore"><a class="more" onclick="pcL01(2);">More</a>

                </li>
            </menu>
            <menu class="toolbarMoreGroup toolbarReferencePoint toolbarImage" style="top: 45px; left: 264px;">
                <li id="BtnImgScaleINRetail" class="elementToolbarLink enabled AnchorMoveUp"><a>Scale up</a></li>
                <li id="BtnImgScaleOutRetail" class="elementToolbarLink enabled AnchorMoveUp"><a>Scale down</a></li>
                <li id="BtnImgRotateLeftRetail" class="elementToolbarLink enabled AnchorMoveUp"><a>Rotate left</a></li>
                <li id="BtnImgRotateRightRetail" class="elementToolbarLink enabled AnchorMoveUp"><a>Rotate right</a></li>
                <li id="BtnImageArrangeOrdr1Retail" class="elementToolbarLink enabled AnchorMoveUp"><a>Bring forward</a></li>
                <li id="BtnImageArrangeOrdr4Retail" class="elementToolbarLink enabled AnchorMoveUp"><a>Send back</a></li>
                <li id="btnImgTransparency" class="elementToolbarLink enabled AnchorMoveUp" onclick="pcL01(3);"><a>Transparency</a></li>
            </menu>
            <menu class="toobarTransparency retailPropPanelsSubMenu toolbarImageTransparency">
                <span>Transparency</span> <span style="margin-left: 10px;">0 </span>
                <div class="transparencySlider"></div>
                <span>100</span>
            </menu>
        </div>
        <div id="DivColorPickerDraggable" class="DivColorPickerDraggable">
            <div class="paletteToolbarWrapper upwards" style="height: 224px; display: block;">
                <menu class="paletteToolbar ">
                    <div class="inner ColorOptionContainer">
                        <menu></menu>


                    </div>
                </menu>
            </div>
            <div class="paletteToolbarWedge upwards" style="top: 224px; left: 0px; display: block;"></div>
            <div id="DivAdvanceColorPanel" class="colorPicker showColorCode" style="top: 215px; left: 82px;">
                <div class="panelItemsRightAligned">
                    <div class="closePanelButtonQuickText" onclick="pcL36('hide','#DivAdvanceColorPanel')">
                        <br />
                    </div>
                </div>
                <div id="DivLblMoveSlider" class="largeText" style="margin-top: 14px; margin-left: 10px;">
                    Move the Slider to Change Colour Percentage
                </div>
                <div id="sliders">
                    <div id="DivColorC">
                    </div>
                    <div id="DivColorM">
                    </div>
                    <div id="DivColorY">
                    </div>
                    <div id="DivColorK">
                    </div>
                </div>
                <div id="ColorPickerPercentageContainer">
                    <label for="DivColorC" id="LblDivColorC">
                        0%</label>
                    <label for="DivColorM" id="LblDivColorM">
                        0%</label>
                    <label for="DivColorY" id="LblDivColorY">
                        0%</label>
                    <label for="DivColorK" id="LblDivColorK">
                        0%</label>
                </div>
                <div class="clear"></div>
                <div id="ColorPickerPalletContainer" class="largeText" style="margin-top: 20px;">

                    <label for="ColorPalletr" id="LblCollarPalet">
                        Click on button to apply
                    </label>
                    <div class="ColorPalletr btnClrPallet" style="background-color: White; margin-top: -12px;" onclick="f2(0,0,0,0,&quot;#ffffff&quot;);f6(0,0,0,0,&quot;#ffffff&quot;);">
                    </div>
                </div>
            </div>
        </div>
        <div id="DivAlignObjs" class="DivAlignObjs colorPicker">
            <div class="closePanelButton" onclick="pcL36('hide','#DivAlignObjs');">
                <br />
            </div>
            <div class="DivTitleLbl">
                <label class="lblGroupProperties titletxt">
                    Group Properties</label>
            </div>
            <div class="AlignObjsDiv">
                <label class="largeText panelTextPropertyRow  ">
                    Align Objects</label>
            </div>
            <div>
                <button id="BtnAlignObjLeft">
                </button>
                <button id="BtnAlignObjCenter">
                </button>
                <button id="BtnAlignObjRight">
                </button>
                <button id="BtnAlignObjTop">
                </button>
                <button id="BtnAlignObjMiddle">
                </button>
                <button id="BtnAlignObjBottom">
                </button>
            </div>
            <div id="divRetailColorGroup" class="propertyPanelControlDiv" style="display: none;">
                <div class="ColorPallet" style="background-color: rgb(2, 3, 2); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(0,0,0,100,&quot;#020302&quot;,&quot;null&quot;);"></div>
                <div class="ColorPallet" style="background-color: rgb(158, 40, 14); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(0,90,100,40,&quot;#9E280E&quot;,&quot;40&quot;);"></div>
                <div class="ColorPallet" style="background-color: rgb(138, 113, 60); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(30,40,80,30,&quot;#8A713C&quot;,&quot;null&quot;);"></div>
                <div class="ColorPallet" style="background-color: rgb(0, 114, 54); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(100,0,100,40,&quot;#007236&quot;,&quot;null&quot;);"></div>
                <div class="ColorPallet" style="background-color: rgb(0, 125, 151); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(100,5,20,30,&quot;#007D97&quot;,&quot;null&quot;);"></div>
                <button id="AddColorGroupRetail" class="" title="Colour picker">MORE</button>
            </div>
        </div>
        <%-- <button class="button editTxtBtn   " id="divEditObj" style="display: none;">Edit</button>--%>
        <img id="placeHolderTxt" src="assets/placeholderTxt.png" />
        <div id="divBkCropTool" class="divBkCropTool">
        </div>
        <div class="NewCropToolCotainer" id="NewCropToolCotainer">
            <div class="CropControls">
                <menu class="crop CroptoolBar underneath" style="transform: translate3d(-3px, -47px, 0px);">
                    <li class="enabled confirmationToolbarOk"><a class="cropButton">Ok</a></li>
                    <li class="enabled confirmationToolbarCancel"><a onclick="pcl20_newCropCls();">Cancel</a></li>
                </menu>
                <%--  <div class="closePanelButtonCropTool" onclick="pcl20_newCropCls();">
				<br>
			</div>--%>
                <img class="cropimage" alt="" src="" cropwidth="200" cropheight="200" />

                <div class="overlayHoverboxContainer"></div>
                <div class="overlayHoverbox">
                    <img class="imgOrignalCrop" alt="" src="" />
                </div>
            </div>

        </div>
    </section>
    <section class="proofsContainer">
        <div id="DivShadow" class="opaqueLayer"></div>
        <div id="LargePreviewer" class="LargePreviewer" title="Full Size Image">
            <iframe id="LargePreviewerIframe" class="LargePreviewerIframe" src="Previewer.aspx"></iframe>
        </div>
        <div id="PreviewerContainer" class="ui-corner-all propertyPanel">
            <a class="PreviewerDownloadPDF" onclick="k9()" target="_blank">Download PDF </a>
            <div class="PreviewerContainerClose button" onclick="e6()">
                Close
            </div>
            <div class="previewerTitle">
                <span class="lightGray">Preview :</span>
            </div>
            <div class="sliderLine">
            </div>
            <div id="Previewer" class="ui-corner-all">
                <div id="sliderFrame">
                </div>
                <div class="sliderLine sliderLineBtm">
                </div>
                <div id="previewProofing">
                    <div class="divTxtProofing">
                        <div class="ConfirmPopupProof">
                            <label class="lblChkSpellings" runat="server" id="lblConfirmSpellings">
                                Confirm spelling and details</label>
                            <input id="chkCheckSpelling" name="chkCheckSpelling" runat="server" class="regular-checkbox-new"
                                type="checkbox" />
                            <label for="chkCheckSpelling" style="display: inline;">
                            </label>
                        </div>
                        <div style="margin-top: 17px;">
                            <label style="visibility: hidden;">
                                <span class="">Send </span>email proofs to
                            </label>
                            <input type="email" name="userEmail1" id="userEmail1" class="clssInputProofing" style="visibility: hidden;" />
                            <input type="email" name="userEmail2" id="userEmail2" class="clssInputProofing" style="display: none;" />
                        </div>
                    </div>
                    <button id="btnNextProofing" class="btnBlueProofing button">
                        NEXT</button>
                </div>
            </div>
        </div>

        <div class="searchLoaderHolder on">
            <span class="g1"></span>
            <span class="g1"></span>
            <span class="g1"></span>
            <span class="g2 on"></span>
            <span class="g2 on"></span>
            <span class="g2 on"></span>
            <span class="g2 on"></span>
            <span class="g1"></span>

        </div>
        <form id="form1" runat="server" class="imageUploadForm" style="display: none;">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div id="uploader">
                <p class="textWhite">
                    You browser doesn't have Flash, Silverlight or HTML5 support.
                </p>
            </div>
            <div id="uploaderButtonDiv" style="display: none;">
                <a id="uploadImgFileBtn1" href="#" onclick="SubmitItem(this)">Click to upload</a>
            </div>
        </form>
    </section>


    <input id="getCopied" style="position: absolute; left: -115px; top: -25px; height: 20px; width: 100px;" />
    <script src="js/p71.js"></script>
    <script src="js/p19.js"></script>
    <script src="js/XMLWriter.js"></script>
    <script src="js/pc1.js"></script>
    <script src="js/pc2.js"></script>
    <script src="js/pc3.js"></script>
    <script src="js/a12/aj9.js" type="text/javascript"></script>
    <script src="js/a12/aj21-v2.js" type="text/javascript"></script>
    <script src="js/a12/aj12.js" type="text/javascript"></script>
    <script src="js/a12/aj1.js" type="text/javascript"></script>
    <script src="js/p101.js" type="text/javascript"></script>
    <script src="js/p1.js"></script>
    <script src="js/p10-v2.js" type="text/javascript"></script>
    <script src="js/p35-v2.js"></script>
    <script src="js/pcf01-35.js"></script>

</body>
</html>
