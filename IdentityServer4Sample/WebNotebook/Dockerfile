FROM microsoft/dotnet:2.2-aspnetcore-runtime
LABEL maintainer="yxl@qq.com"#可以用来定义一些标签数据，比如镜像的作者，镜像的版本等。。
RUN mkdir /data#这个标签后面，就可以执行一些Shell 命令，比如我创建了一个data目录。
COPY ./ /data#将dockerfile所在目录的内容copy到容器的/data目录， dockerfile所在的目录就是上一篇说到的publish文件夹哦，这是你在docker build的时候指定的 . 确定的路径。
WORKDIR /data# 这个和cd 命令是一个意思，切换到 指定的目录，后续的操作都是在这个切换后的目录上进行操作的。
EXPOSE 80 #容器内部监听的端口
CMD [ "dotnet","WebNotebook.dll" ]#这个是容器起来后，在shell上执行的命令参数，比如启动webnotebook.dll 。